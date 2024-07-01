using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Aulas
{
    public class CreateAulaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("CreateAula")
                .WithSummary("Creates a new class")
                .WithDescription("Creates a new class")
                .Produces<Response<FloripaSurfClubCore.Models.Aula>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAulasHandler handler,
            [FromBody] CreateAulaRequest request)
        {
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/v1/aulas/{response.Data.Id}", response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
