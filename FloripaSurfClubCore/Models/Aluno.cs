using FloripaSurfClubCore.Enums;
using System;

namespace FloripaSurfClubCore.Models
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid UsuarioSistemaId { get; set; }
        //public UsuarioSistema UsuarioSistema { get; set; }
        public decimal Peso { get; set; }
        public int Altura { get; set; }
        public string Nacionalidade { get; set; }
        public ENivel Nivel { get; set; }

        public List<Aula> Aulas { get; set; } = new List<Aula>();
    }
}
