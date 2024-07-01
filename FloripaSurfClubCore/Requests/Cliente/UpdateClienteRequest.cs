using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Cliente
{
    public class UpdateClienteRequest : Request
    {
        public  Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorAPagar { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }
}
