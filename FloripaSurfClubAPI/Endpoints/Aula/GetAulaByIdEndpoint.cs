using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Aulas
{
    public class GetAulaByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("GetAulaById")
                .WithSummary("Obtém uma aula pelo ID")
                .WithDescription("Obtém uma aula pelo ID")
                .Produces<Response<Aula>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAulasHandler handler,
            [FromRoute] Guid id)
        {
            var request = new GetAulaByIdRequest { Id = id };
            var response = await handler.GetByIdAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(new { response.Data.Id, response.Data.ProfessorId, response.Message })
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
