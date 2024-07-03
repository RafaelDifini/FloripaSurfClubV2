using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Atendente
{
    public class GetAtendenteByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/{usuarioSistemaId}", HandleAsync)
                .WithName("GetAtendenteById")
                .WithSummary("Obtém um atendente pelo ID")
                .WithDescription("Obtém um atendente pelo ID")
                .Produces<Response<FloripaSurfClubCore.Models.Atendente>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAtendenteHandler handler,
            [FromRoute] Guid usuarioSistemaId)
        {
            var request = new GetAtendenteByIdRequest { UsuarioSistemaId = usuarioSistemaId };
            var response = await handler.GetByIdAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
