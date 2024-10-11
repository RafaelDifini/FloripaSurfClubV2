using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Caixa;
using FloripaSurfClubCore.Responses;

namespace FloripaSurfClubWeb.Handlers
{
    public class CaixaHandler : ICaixaHandler
    {
        public Task<Response<Caixa>> AbrirCaixaAsync(AbrirCaixaRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Caixa>> GetCaixaByIdAsync(GetCaixaByIdRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Caixa?>> UpdateAsync(UpdateCaixaRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
