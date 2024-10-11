using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Responses;
using System.Net.Http.Json;

namespace FloripaSurfClubWeb.Handlers
{
    public class AlunoHandler(IHttpClientFactory httpClientFactory) : IAlunoHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Aluno>> CreateAsync(CreateAlunoRequest request)
        {
            var result = await _client.PostAsJsonAsync("/v1/alunos", request);
            return await result.Content.ReadFromJsonAsync<Response<Aluno>>() ??
                  new Response<Aluno>(null, 400, "Falha ao criar o aluno");
        }

        public async Task<Response<Aluno?>> DeleteAsync(DeleteAlunoRequest request)
        {
            var result = await _client.DeleteAsync($"/v1/alunos/{request.UsuarioSistemaId}");
            return await result.Content.ReadFromJsonAsync<Response<Aluno?>>() ??
                  new Response<Aluno?>(null, 400, "Falha ao excluir o aluno");
        }

        public async Task<Response<List<Aluno>>> GetAllAsync(GetAllAlunosRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<Aluno>>>("v1/alunos") ??
                   new Response<List<Aluno>>(null, 400, "Não foi possível obter a lista de alunos");
        }

        public async Task<Response<Aluno?>> GetByIdAsync(GetAlunoByIdRequest request)
        {
            return await _client.GetFromJsonAsync<Response<Aluno?>>($"v1/alunos/{request.UsuarioSistemaId}") ??
                   new Response<Aluno?>(null, 400, "Não foi possível obter o aluno");
        }

        public async Task<Response<Aluno?>> UpdateAsync(UpdateAlunoRequest request)
        {
            var result = await _client.PutAsJsonAsync("/v1/alunos", request);
            return await result.Content.ReadFromJsonAsync<Response<Aluno?>>() ??
                  new Response<Aluno?>(null, 400, "Falha ao atualizar o aluno");
        }
    }
}
