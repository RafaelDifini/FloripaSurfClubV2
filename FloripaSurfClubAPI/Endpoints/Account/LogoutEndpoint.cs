using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FloripaSurfClubAPI.Endpoints.Account
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app
                .MapPost("/logout", HandleAsync)
                .RequireAuthorization();

        private static async Task<IResult> HandleAsync(SignInManager<UsuarioSistema> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }
}
