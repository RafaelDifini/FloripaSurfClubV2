using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Aulas;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Aulas
{
    public class ObterHorariosDisponiveisEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/horarios-disponiveis", HandleAsync)
                .WithName("ObterHorariosDisponiveis")
                .WithSummary("Obtém os horários disponíveis para um professor em uma data específica")
                .Produces<Response<List<DateTime>>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAulasHandler handler,
            [FromBody] ObterHorariosDisponiveisRequest request)
        {
            if (request == null || request.ProfessorId == Guid.Empty || request.DataSelecionada == default)
            {
                return TypedResults.BadRequest("Dados inválidos.");
            }

            var response = await handler.ObterHorariosDisponiveisAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
