using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

public class AgendarAulaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("AgendarAula")
            .WithSummary("Cria uma nova aula")
            .WithDescription("Cria uma nova aula no sistema")
            .Produces<Response<Aula>>(StatusCodes.Status201Created)
            .Produces<Response<Aula>>(StatusCodes.Status400BadRequest) // A produção de erros agora segue o Response<Aula>
            .Produces<Response<Aula>>(StatusCodes.Status500InternalServerError); // Para manter a consistência

    private static async Task<IResult> HandleAsync(
        [FromServices] IAulasHandler handler,
        [FromBody] CreateAulaRequest request)
    {
        var response = await handler.AgendarAulaAsync(request);
   
        if (response.IsSuccess)
        {
            return TypedResults.Created($"/v1/aulas/{response.Data.Id}", response); 
        }

        return Results.Json(response, statusCode: response._code);
    }
}
