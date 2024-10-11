using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;
using System.Net.Http.Json;

namespace FloripaSurfClubWeb.Handlers
{
    public class AtendenteHandler(IHttpClientFactory httpClientFactory) : IAtendenteHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Atendente>> CreateAsync(CreateAtendenteRequest request)
        {
            var result = await _client.PostAsJsonAsync("/v1/atendentes", request);
            return await result.Content.ReadFromJsonAsync<Response<Atendente>>() ??
                  new Response<Atendente>(null, 400, "Falha ao criar o atendente");
        }

        public async Task<Response<Atendente?>> DeleteAsync(DeleteAtendenteRequest request)
        {
            var result = await _client.DeleteAsync($"/v1/atendentes/{request.UsuarioSistemaId}");
            return await result.Content.ReadFromJsonAsync<Response<Atendente?>>() ??
                  new Response<Atendente?>(null, 400, "Falha ao excluir o atendente");
        }

        public async Task<Response<List<Atendente>>> GetAllAsync(GetAllAtendentesRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<Atendente>>>("v1/atendentes") ??
                   new Response<List<Atendente>>(null, 400, "Não foi possível obter a lista de atendentes");
        }

        public async Task<Response<Atendente?>> GetByIdAsync(GetAtendenteByIdRequest request)
        {
            return await _client.GetFromJsonAsync<Response<Atendente?>>($"v1/atendentes/{request.UsuarioSistemaId}") ??
                   new Response<Atendente?>(null, 400, "Não foi possível obter o atendente");
        }

        public async Task<Response<Atendente?>> UpdateAsync(UpdateAtendenteRequest request)
        {
            var result = await _client.PutAsJsonAsync("/v1/atendentes", request);
            return await result.Content.ReadFromJsonAsync<Response<Atendente?>>() ??
                  new Response<Atendente?>(null, 400, "Falha ao atualizar o atendente");
        }
    }
}
