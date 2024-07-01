using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Handlers
{
    public interface IAulasHandler
    {
        Task<Response<Aula>> CreateAsync(CreateAulaRequest request);
        Task<Response<Aula?>> UpdateAsync(UpdateAulaRequest request);
        Task<Response<Aula?>> DeleteAsync(DeleteAulaRequest request);
        Task<Response<Aula?>> GetByIdAsync(GetAulaByIdRequest request);
        Task<Response<List<Aula>>> GetAllAsync(GetAllAulasRequest request);
    }
}
