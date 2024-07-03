using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Alunos;
using FloripaSurfClubCore.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloripaSurfClubAPI.Handlers
{
    public class AlunoHandler : IAlunoHandler
    {
        private readonly FloripaSurfClubContextV2 _context;

        public AlunoHandler(FloripaSurfClubContextV2 context)
        {
            _context = context;
        }

        public async Task<Response<Aluno>> CreateAsync(CreateAlunoRequest request)
        {
            try
            {
                var aluno = new Aluno
                {
                    Nome = request.Nome,
                    Peso = request.Peso,
                    Altura = request.Altura,
                    Nacionalidade = request.Nacionalidade,
                    Nivel = request.Nivel,
                    UsuarioSistemaId = request.UsuarioSistemaId
                };

                await _context.Alunos.AddAsync(aluno);
                await _context.SaveChangesAsync();

                return new Response<Aluno>(aluno, 201, "Aluno criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Aluno>(null, 500, $"Não foi possível criar o aluno: {ex.Message}");
            }
        }

        public async Task<Response<Aluno?>> UpdateAsync(UpdateAlunoRequest request)
        {
            try
            {
                var aluno = await _context.Alunos
                    .FirstOrDefaultAsync(x=> x.UsuarioSistemaId == request.UsuarioSistemaId);

                if (aluno is null)
                    return new Response<Aluno?>(null, 404, "Aluno não encontrado");

                aluno.Nome = request.Nome;
                aluno.Peso = request.Peso;
                aluno.Altura = request.Altura;
                aluno.Nacionalidade = request.Nacionalidade;
                aluno.Nivel = request.Nivel;

                _context.Alunos.Update(aluno);
                await _context.SaveChangesAsync();

                return new Response<Aluno?>(aluno, 200, "Aluno atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Aluno?>(null, 500, $"Não foi possível atualizar o aluno: {ex.Message}");
            }
        }

        public async Task<Response<Aluno?>> DeleteAsync(DeleteAlunoRequest request)
        {
            try
            {
                var aluno = await _context.Alunos
                    .FirstOrDefaultAsync(x => x.UsuarioSistemaId == request.UsuarioSistemaId);

                if (aluno is null)
                    return new Response<Aluno?>(null, 404, "Aluno não encontrado");

                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();

                return new Response<Aluno?>(aluno, 200, "Aluno excluído com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Aluno?>(null, 500, $"Não foi possível excluir o aluno: {ex.Message}");
            }
        }

        public async Task<Response<Aluno?>> GetByIdAsync(GetAlunoByIdRequest request)
        {
            try
            {
                var aluno = await _context.Alunos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UsuarioSistemaId == request.UsuarioSistemaId);

                return aluno is null
                    ? new Response<Aluno?>(null, 404, "Aluno não encontrado")
                    : new Response<Aluno?>(aluno, 200, "Aluno encontrado");
            }
            catch (Exception ex)
            {
                return new Response<Aluno?>(null, 500, $"Não foi possível recuperar o aluno: {ex.Message}");
            }
        }

        public async Task<Response<List<Aluno>>> GetAllAsync(GetAllAlunosRequest request)
        {
            try
            {
                var alunos = await _context.Alunos
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<Aluno>>(alunos, 200, "Alunos recuperados com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<List<Aluno>>(null, 500, $"Não foi possível recuperar os alunos: {ex.Message}");
            }
        }
    }
}
