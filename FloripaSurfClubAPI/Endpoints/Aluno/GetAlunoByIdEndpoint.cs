using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubAPI.Models;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Alunos
{
    public class GetAlunoByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{usuarioSistemaId}", HandleAsync)
               .WithName("GetAlunoById")
                .WithSummary("Obtém um aluno por ID")
                .WithDescription("Recupera as informações de um aluno específico no sistema")
                .Produces<Response<Aluno>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAlunoHandler handler,
            [FromRoute] Guid usuarioSistemaId)
        {
            var request = new GetAlunoByIdRequest { UsuarioSistemaId = usuarioSistemaId };
            var response = await handler.GetByIdAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
