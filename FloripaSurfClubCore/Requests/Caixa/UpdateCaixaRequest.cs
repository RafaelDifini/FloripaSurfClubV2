using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Caixa
{
    public class UpdateCaixaRequest
    {
        public Guid Id { get; set; }
        public DateTime? DataFechamento { get; set; } = null;
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
