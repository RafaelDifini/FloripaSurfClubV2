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
            => app.MapPut("/", HandleAsync)
                .WithName("UpdateProfessor")
                .WithSummary("Atualiza um professor existente")
                .WithDescription("Atualiza as informaçoes de um professor existente")
                .Produces<Response<Professor>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IProfessorHandler handler,
            [FromBody] UpdateProfessorRequest request)
        {
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                 : Results.Problem(response.Message, statusCode: 500);

        }
    }
}
