using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Handlers
{
    public interface IAlunoHandler
    {
        Task<Response<Aluno>> CreateAsync(CreateAlunoRequest request);
        Task<Response<Aluno?>> UpdateAsync(UpdateAlunoRequest request);
        Task<Response<Aluno?>> DeleteAsync(DeleteAlunoRequest request);
        Task<Response<Aluno?>> GetByIdAsync(GetAlunoByIdRequest request);
        Task<Response<List<Aluno>>> GetAllAsync(GetAllAlunosRequest request);
    }
}
