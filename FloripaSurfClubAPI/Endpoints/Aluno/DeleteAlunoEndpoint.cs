using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Alunos
{
    public class DeleteAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{usuarioSistemaId}", HandleAsync)
                .WithName("DeleteAluno")
                .WithSummary("Deleta um aluno existente")
                .WithDescription("Remove um aluno do sistema")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAlunoHandler handler,
            [FromRoute] Guid usuarioSistemaId)
        {
            var request = new DeleteAlunoRequest { UsuarioSistemaId = usuarioSistemaId };
            var response = await handler.DeleteAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
