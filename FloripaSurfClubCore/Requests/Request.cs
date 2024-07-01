using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests
{
    public abstract class Request
    {
        public Guid UsuarioSistemaId { get; set; } = Guid.Empty;

    }
}
