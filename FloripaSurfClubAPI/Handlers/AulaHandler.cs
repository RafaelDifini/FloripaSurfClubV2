using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Aula;
using FloripaSurfClubCore.Responses;
using FloripaSurfClubCore.Responses.Alunos;
using FloripaSurfClubCore.Responses.Aulas;
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

        public async Task<Response<Aula>> AgendarAulaAsync(CreateAulaRequest request)
        {
            try
            {
                var professorDisponivel = await EstaDisponivel(request.ProfessorId, request.DataInicio);

                if (!professorDisponivel)
                {
                    return new Response<Aula>(null, 400, "Professor não está disponível nesse horário.");
                }

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

                return new Response<Aula>(aula, 201, "Aula agendada com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Aula>(null, 500, $"Não foi possível agendar a aula: {ex.Message}");
            }
        }

        private async Task<bool> EstaDisponivel(Guid professorId, DateTime dataHora)
        {
            dataHora = dataHora.AddMilliseconds(-dataHora.Millisecond);
            var aulaExistente = await _context.Aulas
                .AnyAsync(a => a.ProfessorId == professorId &&
                               a.DataInicio.Year == dataHora.Year &&
                               a.DataInicio.Month == dataHora.Month &&
                               a.DataInicio.Day == dataHora.Day &&
                               a.DataInicio.Hour == dataHora.Hour &&
                               a.DataInicio.Minute == dataHora.Minute);

            return !aulaExistente;
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

        public async Task<Response<AulaResponse?>> GetByIdAsync(GetAulaByIdRequest request)
        {
            try
            {
                var aula = await _context.Aulas
                    .Include(a => a.Professor)
                    .Include(a => a.Alunos)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (aula == null)
                {
                    return new Response<AulaResponse?>(null, 404, "Aula não encontrada");
                }

                var aulaResponse = new AulaResponse
                {
                    Id = aula.Id,
                    ProfessorId = aula.ProfessorId,
                    ProfessorNome = aula.Professor.Nome,
                    DataInicio = aula.DataInicio,
                    Valor = aula.Valor,
                    EhPacote = aula.EhPacote,
                    Concluida = aula.Concluida,
                    Alunos = aula.Alunos.Select(al => new AlunoResponse
                    {
                        Id = al.Id,
                        Nome = al.Nome,
                        Peso = al.Peso,
                        Altura = al.Altura,
                        Nacionalidade = al.Nacionalidade,
                        Nivel = al.Nivel.ToString()
                    }).ToList()
                };

                return new Response<AulaResponse?>(aulaResponse, 200,"Aula encontrada");
            }
            catch (Exception ex)
            {
                return new Response<AulaResponse?>(null, 500, $"Não foi possível recuperar a aula: {ex.Message}");
            }
        }

        public async Task<Response<List<AulaResponse>>> GetAllAsync(GetAllAulasRequest request)
        {
            try
            {
                var aulas = await _context.Aulas
                    .Include(a => a.Professor)
                    .Include(a => a.Alunos)
                    .ToListAsync();

                var aulaResponses = aulas.Select(a => new AulaResponse
                {
                    Id = a.Id,
                    ProfessorId = a.ProfessorId,
                    ProfessorNome = a.Professor.Nome,
                    DataInicio = a.DataInicio,
                    Valor = a.Valor,
                    EhPacote = a.EhPacote,
                    Concluida = a.Concluida,
                    Alunos = a.Alunos.Select(al => new AlunoResponse
                    {
                        Id = al.Id,
                        Nome = al.Nome,
                        Peso = al.Peso,
                        Altura = al.Altura,
                        Nacionalidade = al.Nacionalidade,
                        Nivel = al.Nivel.ToString()
                    }).ToList()
                }).ToList();

                return new Response<List<AulaResponse>>(aulaResponses,200, "Aulas recuperadas com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<List<AulaResponse>>(null, 500, $"Não foi possível recuperar as aulas: {ex.Message}");
            }
        }
    }
}
