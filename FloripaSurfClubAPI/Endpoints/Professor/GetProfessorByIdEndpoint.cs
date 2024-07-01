using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Professores
{
    public class GetProfessorByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{id}", HandleAsync)
                .WithName("GetProfessorById")
                .WithSummary("Gets a professor by ID")
                .WithDescription("Gets a professor by ID")
                .Produces<Response<Professor>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IProfessorHandler handler,
            [FromRoute] Guid id)
        {
            var request = new GetProfessorByIdRequest { Id = id };
            var response = await handler.GetByIdAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                 : Results.Problem(response.Message, statusCode: 500);

        }
    }
}
