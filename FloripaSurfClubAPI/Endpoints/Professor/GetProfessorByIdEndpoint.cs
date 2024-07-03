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
            => app.MapGet("/{usuarioSistemaId}", HandleAsync)
                .WithName("GetProfessorById")
                .WithSummary("Obtem um professor por ID")
                .WithDescription("Obtem um professor por ID")
                .Produces<Response<Professor>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IProfessorHandler handler,
            [FromRoute] Guid usuarioSistemaId)
        {
            var request = new GetProfessorByIdRequest { UsuarioSistemaId = usuarioSistemaId };
            var response = await handler.GetByIdAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                 : Results.Problem(response.Message, statusCode: 500);

        }
    }
}
