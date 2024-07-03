using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Atendente
{
    public class GetAllAtendentesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("GetAllAtendentes")
                .WithSummary("Obtém todos os atendentes")
                .WithDescription("Obtém todos os atendentes")
                .Produces<Response<List<FloripaSurfClubCore.Models.Atendente>>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAtendenteHandler handler)
        {
            var request = new GetAllAtendentesRequest();
            var response = await handler.GetAllAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
