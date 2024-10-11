using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Cliente;
using FloripaSurfClubCore.Responses;
using System.Net.Http.Json;

namespace FloripaSurfClubWeb.Handlers
{
    public class ClienteHandler(IHttpClientFactory httpClientFactory) : IClienteHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Response<Cliente>> CreateAsync(CreateClienteRequest request)
        {
            var result = await _client.PostAsJsonAsync("/v1/clientes", request);
            return await result.Content.ReadFromJsonAsync<Response<Cliente>>() ??
                  new Response<Cliente>(null, 400, "Falha ao criar o cliente");
        }

        public async Task<Response<List<Cliente>>> GetAllAsync(GetAllClientesRequest request)
        {
            return await _client.GetFromJsonAsync<Response<List<Cliente>>>("v1/clientes") ??
                   new Response<List<Cliente>>(null, 400, "Não foi possível obter a lista de clientes");
        }

        public async Task<Response<Cliente?>> GetByIdAsync(GetClienteByIdRequest request)
        {
            return await _client.GetFromJsonAsync<Response<Cliente?>>($"v1/clientes/{request.Id}") ??
                   new Response<Cliente?>(null, 400, "Não foi possível obter o cliente");
        }

        public async Task<Response<Cliente?>> UpdateAsync(UpdateClienteRequest request)
        {
            var result = await _client.PutAsJsonAsync("/v1/clientes", request);
            return await result.Content.ReadFromJsonAsync<Response<Cliente?>>() ??
                  new Response<Cliente?>(null, 400, "Falha ao atualizar o cliente");
        }
    }
}
