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
        [Required(ErrorMessage = "Insira seu Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Insira um telefone de contato")]
        public string Telefone { get; set; }

        [Required(ErrorMessage ="Senha inválida")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirme a senha")]
        [DataType(DataType.Password)]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmaSenha { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }

        public List<ETipoUsuario> TipoUsuarios = Enum.GetValues(typeof(ETipoUsuario)).Cast<ETipoUsuario>().ToList();
        public decimal? Peso { get; set; }
        public int? Altura { get; set; }
        public string? Nacionalidade { get; set; }
        public ENivel? Nivel { get; set; }
    }
}
