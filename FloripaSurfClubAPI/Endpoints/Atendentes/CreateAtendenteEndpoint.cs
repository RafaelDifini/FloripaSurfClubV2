using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;
using FloripaSurfClubCore.Models;

namespace FloripaSurfClubAPI.Endpoints.Atendente
{
    public class CreateAtendenteEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("CreateAtendente")
                .WithSummary("Cria um novo atendente")
                .WithDescription("Cria um novo atendente")
                .Produces<Response<FloripaSurfClubCore.Models.Atendente>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAtendenteHandler handler,
            [FromBody] CreateAtendenteRequest request)
        {
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/v1/atendentes/{response.Data.Id}", response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
