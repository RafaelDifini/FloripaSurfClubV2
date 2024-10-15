using FloripaSurfClubCore.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Linq;
using System.Text.Json;
using FloripaSurfClubCore.Responses;

namespace FloripaSurfClubWeb.Security
{
    public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory) : AuthenticationStateProvider, ICookieAuthenticationStateProvider
    {
        private bool _isAuthenticated = false;
        private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<bool> CheckAuthenticatedAsync()
        {
            await GetAuthenticationStateAsync();
            return _isAuthenticated;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _isAuthenticated = false;

            var user = new ClaimsPrincipal(new ClaimsIdentity());

            var userInfo = await GetUser();
            if (userInfo is null)
                return new AuthenticationState(user);

            var claims = await GetClaimsAsync(userInfo);

            var id = new ClaimsIdentity(claims, nameof(CookieAuthenticationStateProvider));
            user = new ClaimsPrincipal(id);

            _isAuthenticated = true;
            return new AuthenticationState(user);
        }

        public void NotifyAuthenticationStateChanged()
        {
            base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task<UsuarioSistema?> GetUser()
        {
            try
            {
                var response = await _client.GetAsync("v1/identity/user-info");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                // Desserializa o JSON para Response<UsuarioSistema>
                var result = await response.Content.ReadFromJsonAsync<Response<UsuarioSistema>>();

                if (result == null || result.Data == null || string.IsNullOrEmpty(result.Data.Email))
                {
                    // Caso algum dado essencial esteja faltando
                    return null;
                }

                // Retorna o objeto UsuarioSistema que está dentro de Data
                return result.Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter informações do usuário: {ex.Message}");
                return null;
            }
        }




        private async Task<List<Claim>> GetClaimsAsync(UsuarioSistema user)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, user.Email ?? string.Empty),
                new(ClaimTypes.Email, user.Email ?? string.Empty), 
                new(ClaimTypes.NameIdentifier, user.Id.ToString() ?? string.Empty),
                new(ClaimTypes.MobilePhone, user.Telefone ?? string.Empty), 
                new("Nome", user.Nome ?? string.Empty) 
            };

            // Adiciona claims extras, verificando se existem
            claims.AddRange(user.Claims.Where(x => x.Key != ClaimTypes.Name && x.Key != ClaimTypes.Email)
                .Select(x => new Claim(x.Key, x.Value ?? string.Empty)));  // Garante que Value não é null

            RoleClaim[]? roles;
            try
            {
                roles = await _client.GetFromJsonAsync<RoleClaim[]>("v1/identity/roles");
            }
            catch
            {
                return claims;
            }

            claims.AddRange(from role in roles ?? []
                            where !string.IsNullOrEmpty(role.Type) && !string.IsNullOrEmpty(role.Value)
                            select new Claim(role.Type, role.Value, role.ValueType, role.Issuer, role.OriginalIssuer));

            return claims;
        }


    }
}

