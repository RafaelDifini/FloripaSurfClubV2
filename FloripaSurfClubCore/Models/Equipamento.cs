using FloripaSurfClubCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Models
{
    public class Equipamento
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ETipoEquipamento Tipo { get; set; }

    }
}
