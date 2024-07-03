using FloripaSurfClubCore.Enums;
using FloripaSurfClubCore.Requests.Aula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Alunos
{
    public class CreateAlunoRequest : Request
    {
        public string Nome { get; set; }
        public decimal Peso { get; set; }
        public int Altura { get; set; }
        public string Nacionalidade { get; set; }
        public ENivel Nivel { get; set; }
    }
}
