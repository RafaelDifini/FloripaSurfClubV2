using System;
using System.Collections.Generic;

namespace FloripaSurfClubCore.Models
{
    public class Professor
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid UsuarioSistemaId { get; set; }
        //public UsuarioSistema UsuarioSistema { get; set; }
        public decimal ValorAReceber { get; set; }
        public List<Aula> Aulas { get; set; } = new List<Aula>();
    }
}
