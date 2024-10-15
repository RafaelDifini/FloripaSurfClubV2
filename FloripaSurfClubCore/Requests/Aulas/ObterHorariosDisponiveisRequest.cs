using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Aulas
{
    public class ObterHorariosDisponiveisRequest
    {
        public  Guid ProfessorId { get; set; }
        public DateTime DataSelecionada { get; set; }
    }
}
