using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Professores
{
    public class DeleteProfessorEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{id}", HandleAsync)
                .WithName("DeleteProfessor")
                .WithSummary("Deletes an existing professor")
                .WithDescription("Deletes an existing professor")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IProfessorHandler handler,
            [FromRoute] Guid id)
        {
            var request = new DeleteProfessorRequest { Id = id };
            var response = await handler.DeleteAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode:500);
        }
    }
}
