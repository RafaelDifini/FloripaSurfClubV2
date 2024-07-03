using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Caixa;
using FloripaSurfClubCore.Requests.Cliente;
using FloripaSurfClubCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Handlers
{
    public interface ICaixaHandler
    {
        Task<Response<Caixa>> AbrirCaixaAsync(AbrirCaixaRequest request);
        Task<Response<Caixa?>> UpdateAsync(UpdateCaixaRequest request);
    }
}
