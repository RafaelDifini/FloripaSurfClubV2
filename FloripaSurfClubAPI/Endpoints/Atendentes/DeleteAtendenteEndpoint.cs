using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubAPI.Models;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Atendente
{
    public class DeleteAtendenteEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapDelete("/{usuarioSistemaId}", HandleAsync)
                .WithName("DeleteAtendente")
                .WithSummary("Deleta um atendente existente")
                .WithDescription("Deleta um atendente existente")
                .Produces<Response<FloripaSurfClubCore.Models.Atendente>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAtendenteHandler handler,
            [FromRoute] Guid usuarioSistemaId)
        {
            var request = new DeleteAtendenteRequest { UsuarioSistemaId = usuarioSistemaId };
            var response = await handler.DeleteAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
