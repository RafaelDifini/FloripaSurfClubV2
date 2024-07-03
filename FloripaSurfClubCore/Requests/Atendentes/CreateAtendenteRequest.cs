using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Atendentes
{
    public class CreateAtendenteRequest : Request
    {
        public string Nome { get; set; }
        public Guid UsuarioSistemaId { get; set; }
        public decimal ValorAReceber { get; set; }
    }
}
