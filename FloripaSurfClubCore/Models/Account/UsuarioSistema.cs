using FloripaSurfClubCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Models.Account
{
    public class UsuarioSistema
    {
        public string Nome { get; set; }

        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }

        public Dictionary<string, string> Claims { get; set; } = [];

    }
}
