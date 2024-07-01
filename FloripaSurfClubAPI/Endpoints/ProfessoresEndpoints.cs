using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FloripaSurfClubAPI.Endpoints.Professores
{
    public class ProfessoresEndpoints : BaseEndpoint
    {
        public ProfessoresEndpoints(FloripaSurfClubContextV2 context) : base(context) { }

        public static WebApplication MapProfessorEndpoints(WebApplication app)
        {
            var professorGroup = app.MapGroup("/v1/professor").WithTags("Professores");

            professorGroup.MapGet("/{id}", async ([FromServices] ProfessoresEndpoints endpoints, Guid id) =>
            {
                try
                {
                    var professor = await endpoints._context.Professores.Include(p => p.Aulas).FirstOrDefaultAsync(p => p.Id == id);
                    if (professor == null)
                        return Results.NotFound();

                    return Results.Ok(professor);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"{ex.Message}", statusCode: 500);
                }
            })
            .WithName("GetProfessorById")
            .Produces<Professor>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);



            professorGroup.MapGet("/", async ([FromServices] ProfessoresEndpoints endpoints) =>
            {
                try
                {
                    var professores = await endpoints._context.Professores.Include(p => p.Aulas).ToListAsync();
                    return Results.Ok(professores);
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) here as needed
                    return Results.Problem($"{ex.Message}", statusCode: 500);
                }
            })
            .WithName("GetProfessors")
            .Produces<List<Professor>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);



            professorGroup.MapPost("/", async ([FromServices] ProfessoresEndpoints endpoints, [FromBody] Professor professor) =>
            {
                try
                {
                    endpoints._context.Professores.Add(professor);
                    await endpoints._context.SaveChangesAsync();
                    return Results.Created($"/v1/professor/{professor.Id}", professor);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"{ex.Message}", statusCode: 500);
                }
            })
            .WithName("CreateProfessor")
            .Produces<Professor>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status500InternalServerError);



            professorGroup.MapPut("/{id}", async ([FromServices] ProfessoresEndpoints endpoints, Guid id, [FromBody] Professor professor) =>
            {
                try
                {
                    var professorExistente = await endpoints._context.Professores.FirstOrDefaultAsync(p => p.Id == id);
                    if (professorExistente == null)
                        return Results.NotFound();

                    professorExistente.Nome = professor.Nome;
                    professorExistente.ValorAReceber = professor.ValorAReceber;

                    endpoints._context.Entry(professorExistente).State = EntityState.Modified;
                    await endpoints._context.SaveChangesAsync();

                    return Results.Ok(professorExistente);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"{ex.Message}", statusCode: 500);
                }
            })
            .WithName("UpdateProfessor")
            .Produces<Professor>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);



            professorGroup.MapDelete("/{id}", async ([FromServices] ProfessoresEndpoints endpoints, Guid id) =>
            {
                try
                {
                    var professor = await endpoints._context.Professores.FirstOrDefaultAsync(x => x.Id == id);
                    if (professor == null)
                        return Results.NotFound();

                    endpoints._context.Professores.Remove(professor);
                    await endpoints._context.SaveChangesAsync();

                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"{ex.Message}", statusCode: 500);
                }
            })
            .WithName("DeleteProfessor")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            return app;
        }
    }
}
