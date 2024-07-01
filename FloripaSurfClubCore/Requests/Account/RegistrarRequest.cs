 using FloripaSurfClubCore.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloripaSurfClubCore.Requests.Account
{
    public class RegistrarRequest 
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmaSenha { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
        public decimal? Peso { get; set; }
        public int? Altura { get; set; }
        public string? Nacionalidade { get; set; }
        public ENivel? Nivel { get; set; }
    }
}
