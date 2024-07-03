using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Alunos
{
    public class CreateAlunoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/", HandleAsync)
                .WithName("CreateAluno")
                .WithSummary("Cria um novo aluno")
                .WithDescription("Cria um novo aluno no sistema")
                .Produces<Response<Aluno>>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAlunoHandler handler,
            [FromBody] CreateAlunoRequest request)
        {
            var response = await handler.CreateAsync(request);
            return response.IsSuccess
                ? TypedResults.Created($"/v1/alunos/{response.Data.Id}", response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
