using FloripaSurfClubCore.Enums;
using Microsoft.AspNetCore.Identity;

namespace FloripaSurfClubAPI.Models.Account
{
    public class UsuarioSistema : IdentityUser<Guid>
    {
        public string Nome { get; set; }

        public ETipoUsuario TipoUsuario { get; set; }
    }
}
