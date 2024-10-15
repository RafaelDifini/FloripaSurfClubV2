using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models.Account;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FloripaSurfClubAPI.Endpoints.Identity
{
    public class GetUserInfoEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/user-info", HandleAsync)
                .WithName("GetUserInfo")
                .WithSummary("Retrieves information of the logged-in user")
                .WithDescription("Returns the ID, Name, Email, and Phone of the logged-in user")
                .Produces<Response<UsuarioSistema>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAccountHandler handler,
            ClaimsPrincipal userPrincipal)
        {
            var response = await handler.GetUserInfoAsync(userPrincipal);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message, statusCode: 500);
        }
    }
}
