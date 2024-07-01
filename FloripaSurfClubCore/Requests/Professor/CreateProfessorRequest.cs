using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Professor
{
    public class CreateProfessorRequest : Request
    {
        public string Nome { get; set; }
        public decimal ValorAReceber { get; set; }
    }
}
