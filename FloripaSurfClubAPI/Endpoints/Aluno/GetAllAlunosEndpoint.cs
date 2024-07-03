using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Alunos
{
    public class GetAllAlunosEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
               .WithName("GetAllAlunos")
                .WithSummary("Obtém todos os alunos")
                .WithDescription("Recupera as informações de todos os alunos cadastrados no sistema")
                .Produces<Response<List<Aluno>>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAlunoHandler handler)
        {
            var request = new GetAllAlunosRequest();
            var response = await handler.GetAllAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
