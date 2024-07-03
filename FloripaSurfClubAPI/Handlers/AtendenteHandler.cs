using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Atendentes;
using FloripaSurfClubCore.Responses;
using Microsoft.EntityFrameworkCore;

namespace FloripaSurfClubAPI.Handlers
{
    public class AtendenteHandler : IAtendenteHandler
    {
        private readonly FloripaSurfClubContextV2 _context;

        public AtendenteHandler(FloripaSurfClubContextV2 context)
        {
            _context = context;
        }

        public async Task<Response<Atendente>> CreateAsync(CreateAtendenteRequest request)
        {
            try
            {
                var atendente = new Atendente
                {
                    Nome = request.Nome,
                    UsuarioSistemaId = request.UsuarioSistemaId,
                    ValorAReceber = request.ValorAReceber
                };

                await _context.Atendentes.AddAsync(atendente);
                await _context.SaveChangesAsync();

                return new Response<Atendente>(atendente, 201, "Atendente criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Atendente>(null, 500, $"Não foi possível criar o atendente: {ex.Message}");
            }
        }

        public async Task<Response<Atendente?>> UpdateAsync(UpdateAtendenteRequest request)
        {
            try
            {
                var atendente = await _context.Atendentes.FindAsync(request.Id);

                if (atendente is null)
                    return new Response<Atendente?>(null, 404, "Atendente não encontrado");

                atendente.Nome = request.Nome;
                atendente.UsuarioSistemaId = request.UsuarioSistemaId;
                atendente.ValorAReceber = request.ValorAReceber;

                _context.Atendentes.Update(atendente);
                await _context.SaveChangesAsync();

                return new Response<Atendente?>(atendente, 200, "Atendente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Atendente?>(null, 500, $"Não foi possível atualizar o atendente: {ex.Message}");
            }
        }

        public async Task<Response<Atendente?>> GetByIdAsync(GetAtendenteByIdRequest request)
        {
            try
            {
                var atendente = await _context.Atendentes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.UsuarioSistemaId == request.UsuarioSistemaId);

                return atendente is null
                    ? new Response<Atendente?>(null, 404, "Atendente não encontrado")
                    : new Response<Atendente?>(atendente, 200, "Atendente encontrado");
            }
            catch (Exception ex)
            {
                return new Response<Atendente?>(null, 500, $"Não foi possível recuperar o atendente: {ex.Message}");
            }
        }

        public async Task<Response<List<Atendente>>> GetAllAsync(GetAllAtendentesRequest request)
        {
            try
            {
                var atendentes = await _context.Atendentes
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<Atendente>>(atendentes, 200, "Atendentes recuperados com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<List<Atendente>>(null, 500, $"Não foi possível recuperar os atendentes: {ex.Message}");
            }
        }

        public async Task<Response<Atendente?>> DeleteAsync(DeleteAtendenteRequest request)
        {
            try
            {
                var atendente = await _context.Atendentes.FindAsync(request.UsuarioSistemaId);

                if (atendente is null)
                    return new Response<Atendente?>(null, 404, "Atendente não encontrado");

                _context.Atendentes.Remove(atendente);
                await _context.SaveChangesAsync();

                return new Response<Atendente?>(atendente, 200, "Atendente excluído com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Atendente?>(null, 500, $"Não foi possível excluir o atendente: {ex.Message}");
            }
        }
    }
}
