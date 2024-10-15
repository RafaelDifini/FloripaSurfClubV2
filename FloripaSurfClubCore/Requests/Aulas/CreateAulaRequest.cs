using FloripaSurfClubCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Aula
{
    public class CreateAulaRequest
    {
        public Guid ProfessorId { get; set; }
        public List<Guid> AlunosId { get; set; } = new List<Guid>();
        public DateTime? DataInicio { get; set; }
        public decimal Valor { get; set; }
        public bool EhPacote { get; set; } = false;
        public bool Concluida { get; set; }
    }
}
