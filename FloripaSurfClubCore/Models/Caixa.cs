using System;

namespace FloripaSurfClubCore.Models
{
    public class Caixa
    {
        public Guid Id { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; } = null;
        public Guid IdUsuarioAbertura { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorColaboradores { get; set; }

        public decimal ValorEmpresa
        {
            get
            {
                return ValorTotal - ValorColaboradores;
            }
        }
    }
}
