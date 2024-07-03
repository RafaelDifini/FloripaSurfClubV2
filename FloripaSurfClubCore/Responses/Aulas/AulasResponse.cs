using FloripaSurfClubCore.Responses.Alunos;
using System;
using System.Collections.Generic;

namespace FloripaSurfClubCore.Responses.Aulas
{
    public class AulaResponse
    {
        public Guid Id { get; set; }
        public Guid ProfessorId { get; set; }
        public string ProfessorNome { get; set; }
        public DateTime DataInicio { get; set; }
        public decimal Valor { get; set; }
        public bool EhPacote { get; set; }
        public bool Concluida { get; set; }
        public List<AlunoResponse> Alunos { get; set; } = new List<AlunoResponse>();
    }

  
}
