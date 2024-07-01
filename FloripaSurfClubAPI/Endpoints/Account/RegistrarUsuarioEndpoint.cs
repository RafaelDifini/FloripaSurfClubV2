using FloripaSurfClubAPI.Extensions;
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
            => app.MapPost("/v1/usuario/registrar", HandleAsync)
                .WithName("RegisterUser")
                .WithSummary("Registers a new user")
                .WithDescription("Registers a new user")
                .Produces<Response<string>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAccountHandler handler,
            [FromBody] RegistrarRequest request)
        {
            if (!Validator.TryValidateObject(request, new ValidationContext(request), new List<ValidationResult>(), true))
            {
                return TypedResults.BadRequest("Invalid registration data.");
            }

            var response = await handler.RegisterAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
