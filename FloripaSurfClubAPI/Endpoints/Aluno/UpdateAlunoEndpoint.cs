using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Alunos
{
    public class UpdateAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{usuarioSistemaId}", HandleAsync)
                .WithName("UpdateAluno")
                .WithSummary("Atualiza um aluno existente")
                .WithDescription("Atualiza as informações de um aluno existente no sistema")
                .Produces<Response<Aluno>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAlunoHandler handler,
            [FromRoute] Guid usuarioSistemaId,
            [FromBody] UpdateAlunoRequest request)
        {
            request.UsuarioSistemaId = usuarioSistemaId;
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
