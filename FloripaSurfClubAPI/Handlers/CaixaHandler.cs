using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Caixa;
using FloripaSurfClubCore.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FloripaSurfClubAPI.Handlers
{
    public class CaixaHandler : ICaixaHandler
    {
        private readonly FloripaSurfClubContextV2 _context;

        public CaixaHandler(FloripaSurfClubContextV2 context)
        {
            _context = context;
        }

        public async Task<Response<Caixa>> AbrirCaixaAsync(AbrirCaixaRequest request)
        {
            try
            {
                var caixa = new Caixa
                {
                    DataAbertura = request.DataAbertura,
                    IdUsuarioAbertura = request.IdUsuarioAbertura
                };

                await _context.Caixa.AddAsync(caixa);
                await _context.SaveChangesAsync();

                return new Response<Caixa>(caixa, 201, "Caixa aberto com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Caixa>(null, 500, $"Não foi possível abrir o caixa: {ex.Message}");
            }
        }

        public async Task<Response<Caixa?>> UpdateAsync(UpdateCaixaRequest request)
        {
            try
            {
                var caixaExistente = await _context.Caixa.FirstOrDefaultAsync(c => c.Id == request.Id);
                if (caixaExistente == null)
                {
                    return new Response<Caixa?>(null, 404, "Caixa não encontrado");
                }

                caixaExistente.DataFechamento = request.DataFechamento;
                caixaExistente.ValorTotal = request.ValorTotal;
                caixaExistente.ValorColaboradores = request.ValorColaboradores;

                _context.Caixa.Update(caixaExistente);
                await _context.SaveChangesAsync();

                return new Response<Caixa?>(caixaExistente, 200, "Caixa atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Caixa?>(null, 500, $"Não foi possível atualizar o caixa: {ex.Message}");
            }
        }
    }
}
