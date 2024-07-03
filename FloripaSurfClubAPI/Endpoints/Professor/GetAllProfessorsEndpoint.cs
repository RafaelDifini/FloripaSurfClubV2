using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FloripaSurfClubAPI.Endpoints.Professores
{
    public class GetAllProfessorsEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapGet("/", HandleAsync)
                .WithName("GetProfessors")
                .WithSummary("Obtém todos professores")
                .WithDescription("Obtém todos professores")
                .Produces<Response<List<Professor>>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);

        private static async Task<IResult> HandleAsync(
            IProfessorHandler handler)
        {
            var request = new GetAllProfessorsRequest(); 
            var response = await handler.GetAllAsync(request);
            return response.IsSuccess
                ? TypedResults.Ok(response)
                 : Results.Problem(response.Message, statusCode: 500);

        }
    }
}
