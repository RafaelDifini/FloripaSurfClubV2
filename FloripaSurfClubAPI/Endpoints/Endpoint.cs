using FloripaSurfClubAPI.Endpoints.Account;
using FloripaSurfClubAPI.Endpoints.Aulas;
using FloripaSurfClubAPI.Endpoints.Clientes;
using FloripaSurfClubAPI.Endpoints.Professores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace FloripaSurfClubAPI.Extensions
{
    public static class EndpointMapper
    {
        public static void MapEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("/")
                .WithTags("Health Check")
                .MapGet("/", () => new { message = "OK" });

            endpoints.MapGroup("/v1/usuarios")
                .WithTags("Usuarios")
                .MapEndpoint<RegistrarUsuarioEndpoint>()
                .MapEndpoint<LoginUserEndpoint>()
                .MapEndpoint<LogoutEndpoint>();

            endpoints.MapGroup("/v1/professores")
                .WithTags("Professores")
                .MapEndpoint<CreateProfessorEndpoint>()
                .MapEndpoint<UpdateProfessorEndpoint>()
                .MapEndpoint<DeleteProfessorEndpoint>()
                .MapEndpoint<GetProfessorByIdEndpoint>()
                .MapEndpoint<GetAllProfessorsEndpoint>();

            endpoints.MapGroup("/v1/clientes")
               .WithTags("Clientes")
               .MapEndpoint<CreateClienteEndpoint>()
               .MapEndpoint<UpdateClienteEndpoint>()
               .MapEndpoint<GetClienteByIdEndpoint>()
               .MapEndpoint<GetAllClientesEndpoint>();

            endpoints.MapGroup("/v1/aulas")
                .WithTags("Aulas")
                .MapEndpoint<CreateAulaEndpoint>()
                .MapEndpoint<UpdateAulaEndpoint>()
                .MapEndpoint<DeleteAulaEndpoint>()
                .MapEndpoint<GetAulaByIdEndpoint>()
                .MapEndpoint<GetAllAulasEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
