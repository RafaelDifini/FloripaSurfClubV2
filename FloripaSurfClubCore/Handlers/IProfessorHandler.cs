using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Handlers
{
    public interface IProfessorHandler
    {
        Task<Response<Professor>> CreateAsync(CreateProfessorRequest request);
        Task<Response<Professor?>> UpdateAsync(UpdateProfessorRequest request);
        Task<Response<Professor?>> DeleteAsync(DeleteProfessorRequest request);
        Task<Response<Professor?>> GetByIdAsync(GetProfessorByIdRequest request);
        Task<Response<List<Professor>>> GetAllAsync(GetAllProfessorsRequest request);
    }
}
