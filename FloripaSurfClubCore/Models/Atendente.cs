using System;

namespace FloripaSurfClubCore.Models
{
    public class Atendente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid UsuarioSistemaId { get; set; }
        public decimal ValorAReceber { get; set; }
    }
}
