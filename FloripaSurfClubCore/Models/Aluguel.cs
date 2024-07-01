using System;

namespace FloripaSurfClubCore.Models
{
    public class Aluguel
    {
        public Guid Id { get; set; }

        public Guid EquipamentoId { get; set; }
        public Equipamento Equipamento { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public decimal Valor { get; set; }
    }
}
