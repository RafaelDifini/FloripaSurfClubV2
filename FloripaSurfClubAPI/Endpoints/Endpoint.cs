using FloripaSurfClubAPI.Endpoints.Account;
using FloripaSurfClubAPI.Endpoints.Alunos;
using FloripaSurfClubAPI.Endpoints.Atendente;
using FloripaSurfClubAPI.Endpoints.Aulas;
using FloripaSurfClubAPI.Endpoints.Caixa;
using FloripaSurfClubAPI.Endpoints.Clientes;
using FloripaSurfClubAPI.Endpoints.Professores;
using FloripaSurfClubAPI.Models;
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

            //endpoints.MapGroup("v1/identity")
            //    .WithTags("Identity")
            //    .MapIdentityApi<UsuarioSistema>();


            endpoints.MapGroup("/v1/professores")
                .WithTags("Professores")
                .RequireAuthorization()
                .MapEndpoint<CreateProfessorEndpoint>()
                .MapEndpoint<UpdateProfessorEndpoint>()
                .MapEndpoint<DeleteProfessorEndpoint>()
                .MapEndpoint<GetProfessorByIdEndpoint>()
                .MapEndpoint<GetAllProfessorsEndpoint>();

            endpoints.MapGroup("/v1/clientes")
               .WithTags("Clientes")
               .RequireAuthorization()
               .MapEndpoint<CreateClienteEndpoint>()
               .MapEndpoint<UpdateClienteEndpoint>()
               .MapEndpoint<GetClienteByIdEndpoint>()
               .MapEndpoint<GetAllClientesEndpoint>();

            endpoints.MapGroup("/v1/aulas")
                .WithTags("Aulas")
                .RequireAuthorization()
                .MapEndpoint<AgendarAulaEndpoint>()
                .MapEndpoint<UpdateAulaEndpoint>()
                .MapEndpoint<DeleteAulaEndpoint>()
                .MapEndpoint<GetAulaByIdEndpoint>()
                .MapEndpoint<GetAllAulasEndpoint>();

            endpoints.MapGroup("/v1/alunos")
               .WithTags("Alunos")
               .RequireAuthorization()
               .MapEndpoint<CreateAlunoEndpoint>()
               .MapEndpoint<UpdateAlunoEndpoint>()
               .MapEndpoint<DeleteAlunoEndpoint>()
               .MapEndpoint<GetAlunoByIdEndpoint>()
               .MapEndpoint<GetAllAlunosEndpoint>();

            endpoints.MapGroup("/v1/caixa")
               .WithTags("Caixa")
               .RequireAuthorization()
               .MapEndpoint<GetCaixaByIdEndepoint>()
               .MapEndpoint<AbrirCaixaEndpoint>()
               .MapEndpoint<UpdateCaixaEndpoint>();

            endpoints.MapGroup("/v1/atendentes")
                .WithTags("Atendentes")
                .RequireAuthorization()
               .MapEndpoint<CreateAtendenteEndpoint>()
               .MapEndpoint<UpdateAtendenteEndpoint>()
               .MapEndpoint<GetAtendenteByIdEndpoint>()
               .MapEndpoint<GetAllAtendentesEndpoint>()
               .MapEndpoint<DeleteAtendenteEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
