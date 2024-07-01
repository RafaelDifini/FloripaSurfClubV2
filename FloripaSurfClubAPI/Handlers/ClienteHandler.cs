using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubCore.Models;
using FloripaSurfClubCore.Requests.Cliente;
using FloripaSurfClubCore.Requests.Professor;
using FloripaSurfClubCore.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloripaSurfClubAPI.Handlers
{
    public class ClienteHandler : IClienteHandler
    {
        private readonly FloripaSurfClubContextV2 _context;

        public ClienteHandler(FloripaSurfClubContextV2 context)
        {
            _context = context;
        }

        public async Task<Response<Cliente>> CreateAsync(CreateClienteRequest request)
        {
            try
            {
                var cliente = new Cliente
                {
                    Nome = request.Nome,
                    ValorAPagar = request.ValorAPagar,
                    Telefone = request.Telefone,
                    Email = request.Email
                };

                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();

                return new Response<Cliente>(cliente, 201, "Cliente criado com sucesso!");
            }
            catch (Exception ex)
            {
                return new Response<Cliente>(null, 500, $"Não foi possível criar o cliente: {ex.Message}");
            }
        }

        public async Task<Response<Cliente?>> UpdateAsync(UpdateClienteRequest request)
        {
            try
            {
                var cliente = await _context.Clientes
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                if (cliente is null)
                    return new Response<Cliente?>(null, 404, "Cliente não encontrado");

                cliente.Nome = request.Nome;
                cliente.ValorAPagar = request.ValorAPagar;
                cliente.Telefone = request.Telefone;
                cliente.Email = request.Email;

                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();

                return new Response<Cliente?>(cliente, 200, "Cliente atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<Cliente?>(null, 500, $"Não foi possível atualizar o cliente: {ex.Message}");
            }
        }

        public async Task<Response<Cliente?>> GetByIdAsync(GetClienteByIdRequest request)
        {
            try
            {
                var cliente = await _context.Clientes
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id);

                return cliente is null
                    ? new Response<Cliente?>(null, 404, "Cliente não encontrado")
                    : new Response<Cliente?>(cliente, 200, "Cliente encontrado");
            }
            catch (Exception ex)
            {
                return new Response<Cliente?>(null, 500, $"Não foi possível recuperar o cliente: {ex.Message}");
            }
        }

        public async Task<Response<List<Cliente>>> GetAllAsync(GetAllClientesRequest request)
        {
            try
            {
                var clientes = await _context.Clientes
                    .AsNoTracking()
                    .ToListAsync();

                return new Response<List<Cliente>>(clientes, 200, "Clientes recuperados com sucesso");
            }
            catch (Exception ex)
            {
                return new Response<List<Cliente>>(null, 500, $"Não foi possível recuperar os clientes: {ex.Message}");
            }
        }

    }
}
