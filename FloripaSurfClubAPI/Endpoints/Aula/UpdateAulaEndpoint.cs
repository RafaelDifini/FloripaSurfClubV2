using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Aulas
{
    public class UpdateAulaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/", HandleAsync)
                .WithName("UpdateAula")
                .WithSummary("Atualiza uma aula existente")
                .WithDescription("Atualiza uma aula existente")
                .Produces<Response<Aula>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAulasHandler handler,
            [FromBody] UpdateAulaRequest request)
        {
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
