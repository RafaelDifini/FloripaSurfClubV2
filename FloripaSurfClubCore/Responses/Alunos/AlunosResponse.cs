﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Responses.Alunos
{
    public class AlunoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Peso { get; set; }
        public int Altura { get; set; }
        public string Nacionalidade { get; set; }
        public string Nivel { get; set; }
    }
}
