using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using System.Net.Http.Json;

namespace FloripaSurfClubWeb.Handlers
{
    public class ProfessorHandler(IHttpClientFactory httpClientFactory) : IProfessorHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Professor>> CreateAsync(CreateProfessorRequest request)
        {
            var result = await _client.PostAsJsonAsync("/v1/professores", request);
            return await result.Content.ReadFromJsonAsync<Response<Professor>>() ??
                  new Response<Professor>(null, 400, "Falha ao criar o professor");
        }

        public async Task<Response<Professor?>> DeleteAsync(DeleteProfessorRequest request)
        {
            var result = await _client.DeleteAsync($"/v1/professores/{request.UsuarioSistemaId}");
            return await result.Content.ReadFromJsonAsync<Response<Professor?>>() ??
                  new Response<Professor?>(null, 400, "Falha ao excluir o professor");
        }

        public async Task<Response<List<Professor>>> GetAllAsync(GetAllProfessorsRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<Professor>>>("v1/professores") ??
                   new Response<List<Professor>>(null, 400, "Não foi possível obter a lista de professores");
        }

        public async Task<Response<Professor?>> GetByIdAsync(GetProfessorByIdRequest request)
        {
            return await _client.GetFromJsonAsync<Response<Professor?>>($"v1/professores/{request.UsuarioSistemaId}") ??
                   new Response<Professor?>(null, 400, "Não foi possível obter o professor");
        }

        public async Task<Response<Professor?>> UpdateAsync(UpdateProfessorRequest request)
        {
            var result = await _client.PutAsJsonAsync("/v1/professores", request);
            return await result.Content.ReadFromJsonAsync<Response<Professor?>>() ??
                  new Response<Professor?>(null, 400, "Falha ao atualizar o professor");
        }
    }
}
