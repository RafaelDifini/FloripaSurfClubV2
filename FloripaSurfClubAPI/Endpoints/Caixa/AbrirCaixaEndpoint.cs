using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Caixa;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Caixa
{
    public class AbrirCaixaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("AbrirCaixa")
                .WithSummary("Abre um novo caixa")
                .WithDescription("Abre um novo caixa")
                .Produces<Response<FloripaSurfClubCore.Models.Caixa>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] ICaixaHandler handler,
            [FromBody] AbrirCaixaRequest request)
        {
            var response = await handler.AbrirCaixaAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/v1/caixa/{response.Data.Id}", response)
                : Results.Problem(response.Message, statusCode:500);
        }
    }
}
