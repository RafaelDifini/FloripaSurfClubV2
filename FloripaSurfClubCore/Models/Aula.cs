using System;
using System.Collections.Generic;

namespace FloripaSurfClubCore.Models
{
    public class Aula
    {
        public Guid Id { get; set; }
        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; }
        public List<Aluno> Alunos { get; set; } = new List<Aluno>();
        public DateTime DataInicio { get; set; }
        public decimal Valor { get; set; }
        public bool EhPacote { get; set; } = false;
        public bool Concluida { get; set; }
    }
}
