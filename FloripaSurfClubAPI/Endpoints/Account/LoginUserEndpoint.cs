using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Requests.Account;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FloripaSurfClubAPI.Endpoints.Account
{
    public class LoginUserEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPost("/login", HandleAsync)
                .WithName("LoginUser")
                .WithSummary("Logs in a user")
                .WithDescription("Logs in a user")
                .Produces<Response<string>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            [FromServices] IAccountHandler handler,
            [FromBody] LoginRequest request)
        {
            if (!Validator.TryValidateObject(request, new ValidationContext(request), new List<ValidationResult>(), true))
            {
                return TypedResults.BadRequest("Invalid login data.");
            }

            var response = await handler.LoginAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                : Results.Problem(response.Message);
        }
    }
}
