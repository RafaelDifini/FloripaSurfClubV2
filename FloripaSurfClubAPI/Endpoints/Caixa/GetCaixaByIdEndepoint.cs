using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Requests.Caixa;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Caixa
{
    public class GetCaixaByIdEndepoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
           => app.MapGet("/{id}", HandleAsync)
               .WithName("GetCaixaById")
               .WithSummary("Obtém o caixa pelo ID")
               .WithDescription("Obtém o caixa pelo ID")
               .Produces<Response<FloripaSurfClubCore.Models.Caixa>>(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound)
               .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] ICaixaHandler handler,
             [FromRoute] Guid id)
        {
            var request = new GetCaixaByIdRequest();
            request.Id = id;
            var response = await handler.GetCaixaByIdAsync(request);
            return response.IsSuccess
                  ? TypedResults.Ok(response)
                  : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
