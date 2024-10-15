using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models.Account;
using FloripaSurfClubCore.Requests;
using FloripaSurfClubCore.Requests.Account;
using FloripaSurfClubCore.Responses;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;

namespace FloripaSurfClubWeb.Handlers
{
    public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<UsuarioSistema>> GetUserInfoAsync(ClaimsPrincipal claimsPrincipal)
        {
            var result = await _client.PostAsJsonAsync("v1/identity/user-info", claimsPrincipal);

            if (result.IsSuccessStatusCode)
            {
                var usuario = await result.Content.ReadFromJsonAsync<UsuarioSistema>();

                return usuario != null
                    ? new Response<UsuarioSistema>(usuario, 200, "Informações do usuário recuperadas com sucesso")
                    : new Response<UsuarioSistema>(null, 400, "Não foi possível recuperar as informações do usuário");
            }

            return new Response<UsuarioSistema>(null, 400, "Não foi possível realizar a requisição.");
        }


        public async Task<Response<string>> LoginAsync(LoginRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/usuarios/login", request);
            return result.IsSuccessStatusCode
                ? new Response<string>("Login realizado com sucesso!", 200, "Login realizado com sucesso")
                : new Response<string>("", 400, "Não foi possivel realizar o login");
        }

        public async Task LogoutAsync()
        {
            var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");

            await _client.PostAsJsonAsync("v1/usuarios/logout", emptyContent);
        }

        public async Task<Response<string>> RegisterAsync(RegistrarRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/usuarios/registrar", request);
            return result.IsSuccessStatusCode
                ? new Response<string>("Cadastro realizado com sucesso!", 201, "Cadastro realizado com sucesso")
                : new Response<string>("", 400, "Não foi possivel realizar o seu cadastro");
        }
    }
}
