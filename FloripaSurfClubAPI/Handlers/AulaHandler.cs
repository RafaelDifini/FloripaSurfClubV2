using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloripaSurfClubAPI.Handlers
{
    public class AulaHandler : IAulasHandler
    {
        private readonly FloripaSurfClubContextV2 _context;

        public AulaHandler(FloripaSurfClubContextV2 context)
        {
            _context = context;
        }

        public async Task<Response<Aula>> CreateAsync(CreateAulaRequest request)
        {
            try
            {
                var aula = new Aula
                {
                    ProfessorId = request.ProfessorId,
                    DataInicio = request.DataInicio,
                    Valor = request.Valor,
                    EhPacote = request.EhPacote,
                    Concluida = request.Concluida
                };

                foreach (var alunoId in request.AlunosId)
                {
                    var aluno = await _context.Alunos.FindAsync(alunoId);
                    if (aluno != null)
                    {
                        aula.Alunos.Add(aluno);
                    }
                }

                await _context.Aulas.AddAsync(aula);
                await _context.SaveChangesAsync();

                return new Response<Aula>(aula, 201, "Aula criada com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Aula>(null, 500, $"Não foi possível criar a aula: {ex.Message}");
            }
        }

        public async Task<Response<Aula?>> UpdateAsync(UpdateAulaRequest request)
        {
            try
            {
                var aula = await _context.Aulas
                    .Include(a => a.Alunos)
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (aula is null)
                    return new Response<Aula?>(null, 404, "Aula não encontrada");

                aula.ProfessorId = request.ProfessorId;
                aula.DataInicio = request.DataInicio;
                aula.Valor = request.Valor;
                aula.EhPacote = request.EhPacote;
                aula.Concluida = request.Concluida;

                aula.Alunos.Clear();
                foreach (var alunoId in request.AlunosId)
                {
                    var aluno = await _context.Alunos.FindAsync(alunoId);
                    if (aluno != null)
                    {
                        aula.Alunos.Add(aluno);
                    }
                }

                _context.Aulas.Update(aula);
                await _context.SaveChangesAsync();

                return new Response<Aula?>(aula, 200, "Aula atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Aula?>(null, 500, $"Não foi possível atualizar a aula: {ex.Message}");
            }
        }

        public async Task<Response<Aula?>> DeleteAsync(DeleteAulaRequest request)
        {
            try
            {
                var aula = await _context.Aulas
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (aula is null)
                    return new Response<Aula?>(null, 404, "Aula não encontrada");

                _context.Aulas.Remove(aula);
                await _context.SaveChangesAsync();

                return new Response<Aula?>(aula, 200, "Aula excluída com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Aula?>(null, 500, $"Não foi possível excluir a aula: {ex.Message}");
            }
        }

        public async Task<Response<Aula?>> GetByIdAsync(GetAulaByIdRequest request)
        {
            try
            {
                var aula = await _context.Aulas
                    .Include(a => a.Professor)
                    .Include(a => a.Alunos)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return aula is null
                    ? new Response<Aula?>(null, 404, "Aula não encontrada")
                    : new Response<Aula?>(aula, 200, "Aula encontrada");
            }
            catch (Exception ex)
            {
                return new Response<Aula?>(null, 500, $"Não foi possível recuperar a aula: {ex.Message}");
            }
        }

        public async Task<Response<List<Aula>>> GetAllAsync(GetAllAulasRequest request)
        {
            try
            {
                var aulas = await _context.Aulas
                    .Include(a => a.Professor)
                    .Include(a => a.Alunos)
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<Aula>>(aulas, 200, "Aulas recuperadas com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<List<Aula>>(null, 500, $"Não foi possível recuperar as aulas: {ex.Message}");
            }
        }
    }
}
