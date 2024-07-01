using FloripaSurfClubAPI.Models;
using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Enums;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FloripaSurfClubAPI.Endpoints
{
    public class UsuarioEndpoints : BaseEndpoint
    {
        private readonly UserManager<UsuarioSistema> _userManager;
        private readonly SignInManager<UsuarioSistema> _signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UsuarioEndpoints(
            FloripaSurfClubContextV2 context,
            UserManager<UsuarioSistema> userManager,
            SignInManager<UsuarioSistema> signInManager,
            RoleManager<IdentityRole<Guid>> roleManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public static WebApplication MapUsuarioEndpoints(WebApplication app)
        {
            var usuarioGroup = app.MapGroup("/v1/usuario").WithTags("Usuarios");

            usuarioGroup.MapPost("/registrar", async ([FromServices] UsuarioEndpoints endpoints, [FromBody] RegistrarRequest usuarioInputModel) =>
            {
                if (!Validator.TryValidateObject(usuarioInputModel, new ValidationContext(usuarioInputModel), new List<ValidationResult>(), true))
                {
                    return Results.BadRequest("Invalid registration data.");
                }

                try
                {
                    var usuario = new UsuarioSistema
                    {
                        UserName = usuarioInputModel.Email,
                        Email = usuarioInputModel.Email,
                        Nome = usuarioInputModel.Nome,
                        PhoneNumber = usuarioInputModel.Telefone,
                        TipoUsuario = usuarioInputModel.TipoUsuario
                    };

                    var result = await endpoints._userManager.CreateAsync(usuario, usuarioInputModel.Senha);

                    if (result.Succeeded)
                    {
                        switch (usuarioInputModel.TipoUsuario)
                        {
                            case ETipoUsuario.Aluno:
                                var aluno = new Aluno
                                {
                                    UsuarioSistemaId = usuario.Id,
                                    Nome = usuarioInputModel.Nome,
                                    Peso = usuarioInputModel.Peso.Value,
                                    Altura = usuarioInputModel.Altura.Value,
                                    Nacionalidade = usuarioInputModel.Nacionalidade,
                                    Nivel = usuarioInputModel.Nivel.Value
                                };
                                endpoints._context.Alunos.Add(aluno);
                                break;
                            case ETipoUsuario.Professor:
                                var professor = new Professor
                                {
                                    UsuarioSistemaId = usuario.Id,
                                    Nome = usuarioInputModel.Nome,
                                    ValorAReceber = 0
                                };
                                endpoints._context.Professores.Add(professor);
                                break;
                            case ETipoUsuario.Atendente:
                                var atendente = new Atendente
                                {
                                    UsuarioSistemaId = usuario.Id,
                                    Nome = usuarioInputModel.Nome,
                                    ValorAReceber = 0
                                };
                                endpoints._context.Atendentes.Add(atendente);
                                break;
                            case ETipoUsuario.Cliente:
                                var cliente = new Cliente
                                {
                                    Nome = usuarioInputModel.Nome,
                                    ValorAPagar = 0,
                                    Email = usuarioInputModel.Email,
                                    Telefone = usuarioInputModel.Telefone
                                };
                                endpoints._context.Clientes.Add(cliente);
                                break;
                            default:
                                return Results.BadRequest("Tipo de usuário inválido.");
                        }

                        await endpoints._context.SaveChangesAsync();

                        return Results.Ok("User registered successfully.");
                    }

                    return Results.BadRequest("User registration failed.");
                }
                catch (Exception ex)
                {
                    return Results.Problem($"{ex.Message}", statusCode: 500);
                }
            })
            .AllowAnonymous()
            .WithName("RegisterUser")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

            return app;
        }
    }
}
