using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Caixa
{
    public class AbrirCaixaRequest
    {
        public DateTime DataAbertura { get; set; }
        public Guid IdUsuarioAbertura { get; set; }
    }
}
