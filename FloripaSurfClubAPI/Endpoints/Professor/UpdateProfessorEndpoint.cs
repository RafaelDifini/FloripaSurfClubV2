using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Professores
{
    public class UpdateProfessorEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandleAsync)
                .WithName("UpdateProfessor")
                .WithSummary("Updates an existing professor")
                .WithDescription("Updates an existing professor")
                .Produces<Response<Professor>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IProfessorHandler handler,
            [FromRoute] Guid id,
            [FromBody] UpdateProfessorRequest request)
        {
            request.Id = id;
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                 : Results.Problem(response.Message, statusCode: 500);

        }
    }
}
