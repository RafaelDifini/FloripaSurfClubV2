using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using FloripaSurfClubCore.Responses.Aulas;
using System.Net.Http.Json;

namespace FloripaSurfClubWeb.Handlers
{
    public class AulasHandler(IHttpClientFactory httpClientFactory) : IAulasHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<Response<Aula>> AgendarAulaAsync(CreateAulaRequest request)
        {
            var result = await _client.PostAsJsonAsync("/v1/aulas", request);
            return await result.Content.ReadFromJsonAsync<Response<Aula>>() ??
                  new Response<Aula>(null, 400, "Falha ao agendar a aula");
        }

        public async Task<Response<Aula>> DeleteAsync(DeleteAulaRequest request)
        {
            var result = await _client.DeleteAsync($"/v1/aulas{request.Id}");
            return await result.Content.ReadFromJsonAsync<Response<Aula>>() ??
                  new Response<Aula>(null, 400, "Falha ao excluir a aula");
        }

        public async Task<Response<List<AulaResponse>>> GetAllAsync(GetAllAulasRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<AulaResponse>>>("v1/aula")
                ?? new Response<List<AulaResponse>>(null, 400, "Não foi possível obter as aulas");
        }

        public async Task<Response<AulaResponse?>> GetByIdAsync(GetAulaByIdRequest request)
        {
            return await _client.GetFromJsonAsync<Response<AulaResponse?>>($"v1/aula/{request.Id}") 
                ?? new Response<AulaResponse?>(null, 400, "Não foi possível obter a aula");
        }

        public async Task<Response<Aula>> UpdateAsync(UpdateAulaRequest request)
        {
            var result = await _client.PutAsJsonAsync("/v1/aulas", request);
            return await result.Content.ReadFromJsonAsync<Response<Aula>>() ??
                  new Response<Aula>(null, 400, "Falha ao atualizar a aula");
        }
    }
}
