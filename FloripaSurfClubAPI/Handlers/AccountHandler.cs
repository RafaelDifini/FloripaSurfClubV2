using FloripaSurfClubAPI.Models.Account;
using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Enums;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Account;
using FloripaSurfClubCore.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FloripaSurfClubAPI.Handlers
{
    public class AccountHandler : IAccountHandler
    {
        private readonly FloripaSurfClubContextV2 _context;
        private readonly UserManager<UsuarioSistema> _userManager;
        private readonly SignInManager<UsuarioSistema> _signInManager;

        public AccountHandler(
            FloripaSurfClubContextV2 context,
            UserManager<UsuarioSistema> userManager,
            SignInManager<UsuarioSistema> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, false);

                if (result.Succeeded)
                {
                    return new Response<string>("Login successful", 200, "User logged in successfully.");
                }

                return new Response<string>(null, 400, "Invalid login attempt.");
            }
            catch (Exception ex)
            {
                return new Response<string>(null, 500, $"An error occurred while logging in: {ex.Message}");
            }
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Response<string>> RegisterAsync(RegistrarRequest request)
        {
            try
            {
                var usuario = new UsuarioSistema
                {
                    UserName = request.Email,
                    Email = request.Email,
                    Nome = request.Nome,
                    PhoneNumber = request.Telefone,
                    TipoUsuario = request.TipoUsuario
                };

                var result = await _userManager.CreateAsync(usuario, request.Senha);

                if (result.Succeeded)
                {
                    switch (request.TipoUsuario)
                    {
                        case ETipoUsuario.Aluno:
                            var aluno = new Aluno
                            {
                                UsuarioSistemaId = usuario.Id,
                                Nome = request.Nome,
                                Peso = request.Peso.Value,
                                Altura = request.Altura.Value,
                                Nacionalidade = request.Nacionalidade,
                                Nivel = request.Nivel.Value
                            };
                            _context.Alunos.Add(aluno);
                            break;
                        case ETipoUsuario.Professor:
                            var professor = new Professor
                            {
                                UsuarioSistemaId = usuario.Id,
                                Nome = request.Nome,
                                ValorAReceber = 0
                            };
                            _context.Professores.Add(professor);
                            break;
                        case ETipoUsuario.Atendente:
                            var atendente = new Atendente
                            {
                                UsuarioSistemaId = usuario.Id,
                                Nome = request.Nome,
                                ValorAReceber = 0
                            };
                            _context.Atendentes.Add(atendente);
                            break;
                        case ETipoUsuario.Cliente:
                            var cliente = new Cliente
                            {
                                Nome = request.Nome,
                                ValorAPagar = 0,
                                Email = request.Email,
                                Telefone = request.Telefone
                            };
                            _context.Clientes.Add(cliente);
                            break;
                        default:
                            return new Response<string>(null, 400, "Invalid user type.");
                    }

                    await _context.SaveChangesAsync();

                    return new Response<string>("User registered successfully", 201, $"Id do usuário: {usuario.Id}");
                }

                return new Response<string>(null, 400, "User registration failed.");
            }
            catch (Exception ex)
            {
                return new Response<string>(null, 500, $"An error occurred while registering the user: {ex.Message}");
            }
        }
    }
}
