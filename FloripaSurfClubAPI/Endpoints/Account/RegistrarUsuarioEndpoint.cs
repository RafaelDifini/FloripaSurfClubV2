using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubAPI.Models;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Account;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FloripaSurfClubAPI.Endpoints.Account
{
    public class RegistrarUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/registrar", HandleAsync)
                .WithName("RegisterUser")
                .WithSummary("Registra um novo usuário")
                .WithDescription("Registra um novo usuário\"")
                .Produces<Response<UsuarioSistema>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError)
                .RequireAuthorization();

        private static async Task<IResult> HandleAsync(
            [FromServices] IAccountHandler handler,
            [FromBody] RegistrarRequest request)
        {
            if (!Validator.TryValidateObject(request, new ValidationContext(request), new List<ValidationResult>(), true))
            {
                return TypedResults.BadRequest("Dados inválidos.");
            }

            var response = await handler.RegisterAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/v1/usuarios/{response.Data}", response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
