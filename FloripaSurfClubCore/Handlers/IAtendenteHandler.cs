using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;

namespace FloripaSurfClubCore.Handlers
{
    public interface IAtendenteHandler
    {
        Task<Response<Atendente>> CreateAsync(CreateAtendenteRequest request);
        Task<Response<Atendente?>> UpdateAsync(UpdateAtendenteRequest request);
        Task<Response<Atendente?>> GetByIdAsync(GetAtendenteByIdRequest request);
        Task<Response<List<Atendente>>> GetAllAsync(GetAllAtendentesRequest request);
        Task<Response<Atendente?>> DeleteAsync(DeleteAtendenteRequest request);
    }
}
