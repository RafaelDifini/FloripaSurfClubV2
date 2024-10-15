using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Requests.Aulas;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using FloripaSurfClubCore.Responses.Aulas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Handlers
{
    public interface IAulasHandler
    {
        Task<Response<Aula>> AgendarAulaAsync(CreateAulaRequest request);
        Task<Response<Aula>> UpdateAsync(UpdateAulaRequest request);
        Task<Response<Aula>> DeleteAsync(DeleteAulaRequest request);
        Task<Response<AulaResponse?>> GetByIdAsync(GetAulaByIdRequest request);
        Task<Response<List<AulaResponse>>> GetAllAsync(GetAllAulasRequest request);

        Task<Response<List<DateTime>>> ObterHorariosDisponiveisAsync(ObterHorariosDisponiveisRequest request);
    }
}
