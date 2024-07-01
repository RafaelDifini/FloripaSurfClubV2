using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Professores
{
    public class CreateProfessorEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("CreateProfessor")
                .WithSummary("Creates a new professor")
                .WithDescription("Creates a new professor")
                .Produces<Response<Professor>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IProfessorHandler handler,
            [FromBody] CreateProfessorRequest request)
        {
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/{response.Data.Id}", response)
                 : Results.Problem(response.Message, statusCode: 500);

        }
    }
}
