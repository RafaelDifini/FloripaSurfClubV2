using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Aulas
{
    public class AgendarAulaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("AgendarAula")
                .WithSummary("Cria uma nova aula")
                .WithDescription("Cria uma nova aula no sistema")
                .Produces<Response<Aula>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAulasHandler handler,
            [FromBody] CreateAulaRequest request)
        {
            var response = await handler.AgendarAulaAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/v1/aulas/{response.Data.Id}", new { response.Data.Id, response.Message })
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
