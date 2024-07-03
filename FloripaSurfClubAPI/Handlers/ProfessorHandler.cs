using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloripaSurfClubAPI.Handlers
{
    public class ProfessorHandler : IProfessorHandler
    {
        private readonly FloripaSurfClubContextV2 _context;

        public ProfessorHandler(FloripaSurfClubContextV2 context)
        {
            _context = context;
        }

        public async Task<Response<Professor>> CreateAsync(CreateProfessorRequest request)
        {
            try
            {
                var professor = new Professor
                {
                    UsuarioSistemaId = request.UsuarioSistemaId,
                    Nome = request.Nome,
                    ValorAReceber = request.ValorAReceber
                };

                await _context.Professores.AddAsync(professor);
                await _context.SaveChangesAsync();

                return new Response<Professor>(professor, 201, "Professor criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Professor>(null, 500, $"Não foi possível criar o professor: {ex.Message}");
            }
        }

        public async Task<Response<Professor?>> UpdateAsync(UpdateProfessorRequest request)
        {
            try
            {
                var professor = await _context.Professores
                    .FirstOrDefaultAsync(x => x.UsuarioSistemaId == request.UsuarioSistemaId);

                if (professor is null)
                    return new Response<Professor?>(null, 404, "Professor não encontrado");

                professor.Nome = request.Nome;
                professor.ValorAReceber = request.ValorAReceber;

                _context.Professores.Update(professor);
                await _context.SaveChangesAsync();

                return new Response<Professor?>(professor, 200, "Professor atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Professor?>(null, 500, $"Não foi possível atualizar o professor: {ex.Message}");
            }
        }

        public async Task<Response<Professor?>> DeleteAsync(DeleteProfessorRequest request)
        {
            try
            {
                var professor = await _context.Professores
                    .FirstOrDefaultAsync(x => x.UsuarioSistemaId == request.UsuarioSistemaId);

                if (professor is null)
                    return new Response<Professor?>(null, 404, "Professor não encontrado");

                _context.Professores.Remove(professor);
                await _context.SaveChangesAsync();

                return new Response<Professor?>(professor, 200, "Professor excluído com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Professor?>(null, 500, $"Não foi possível excluir o professor: {ex.Message}");
            }
        }

        public async Task<Response<Professor?>> GetByIdAsync(GetProfessorByIdRequest request)
        {
            try
            {
                var professor = await _context.Professores
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UsuarioSistemaId == request.UsuarioSistemaId);

                return professor is null
                    ? new Response<Professor?>(null, 404, "Professor não encontrado")
                    : new Response<Professor?>(professor, 200, "Professor encontrado");
            }
            catch (Exception ex)
            {
                return new Response<Professor?>(null, 500, $"Não foi possível recuperar o professor: {ex.Message}");
            }
        }

        public async Task<Response<List<Professor>>> GetAllAsync(GetAllProfessorsRequest request)
        {
            try
            {
                var professores = await _context.Professores
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<Professor>>(professores, 200, "Professores recuperados com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<List<Professor>>(null, 500, $"Não foi possível recuperar os professores: {ex.Message}");
            }
        }

        private async Task<bool> EstaDisponivel(Guid professorId, DateTime dataHora)
        {
            var aulaExistente = await _context.Aulas
                .AnyAsync(a => a.ProfessorId == professorId &&
                               a.DataInicio.Year == dataHora.Year &&
                               a.DataInicio.Month == dataHora.Month &&
                               a.DataInicio.Day == dataHora.Day &&
                               a.DataInicio.Hour == dataHora.Hour &&
                               a.DataInicio.Minute == dataHora.Minute);

            return !aulaExistente;
        }
    }
}
