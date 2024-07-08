using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Caixa;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Caixa
{
    public class UpdateCaixaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/", HandleAsync)
                .WithName("UpdateCaixa")
                .WithSummary("Atualiza um caixa existente")
                .WithDescription("Atualiza um caixa existente")
                .Produces<Response<FloripaSurfClubCore.Models.Caixa>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] ICaixaHandler handler,
            [FromBody] UpdateCaixaRequest request)
        {
            var response = await handler.UpdateAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode:  500);
        }
    }
}
