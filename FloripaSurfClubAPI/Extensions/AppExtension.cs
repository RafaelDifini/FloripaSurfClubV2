using FloripaSurfClubAPI.Endpoints;
using FloripaSurfClubAPI.Endpoints.Professores;
using System.Runtime.CompilerServices;

namespace FloripaSurfClubAPI.Extensions
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvironment(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty; // Define Swagger como a página inicial
            });
        }

        public static void MapEndpoinst(this WebApplication app)
        {
            ProfessoresEndpoints.MapProfessorEndpoints(app);
            UsuarioEndpoints.MapUsuarioEndpoints(app);
        }
    }
}
