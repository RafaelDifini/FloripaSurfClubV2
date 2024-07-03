using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Cliente;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Clientes
{
    public class CreateClienteEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("CreateCliente")
                .WithSummary("Cria um novo cliente")
                .WithDescription("Cria um novo cliente")
                .Produces<Response<Cliente>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IClienteHandler handler,
            [FromBody] CreateClienteRequest request)
        {
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/v1/clientes/{response.Data.Id}", response)
                 : Results.Problem(response.Message, statusCode: 500);

        }
    }
}
