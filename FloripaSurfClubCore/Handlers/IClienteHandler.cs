using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Cliente;
using FloripaSurfClubCore.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Handlers
{
    public interface IClienteHandler
    {
        Task<Response<Cliente>> CreateAsync(CreateClienteRequest request);
        Task<Response<Cliente?>> UpdateAsync(UpdateClienteRequest request);
        Task<Response<Cliente?>> GetByIdAsync(GetClienteByIdRequest request);
        Task<Response<List<Cliente>>> GetAllAsync(GetAllClientesRequest request);
    }
}
