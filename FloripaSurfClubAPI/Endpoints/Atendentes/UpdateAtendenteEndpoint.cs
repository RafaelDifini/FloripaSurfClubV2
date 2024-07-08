using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubAPI.Models;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Atendente
{
    public class UpdateAtendenteEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/", HandleAsync)
                .WithName("UpdateAtendente")
                .WithSummary("Atualiza um atendente existente")
                .WithDescription("Atualiza um atendente existente")
                .Produces<Response<FloripaSurfClubCore.Models.Atendente>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAtendenteHandler handler,
            [FromBody] UpdateAtendenteRequest request)
        {
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
