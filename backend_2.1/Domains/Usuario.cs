using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace senai_2s2019_CodeXP_Gufos.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Presenca = new HashSet<Presenca>();
        }

        public int UsuarioId { get; set; }
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "A senha deve conter no mínimo 3 caracteres")]
        public string Senha { get; set; }
        public int? TipoUsuarioId { get; set; }

        public TipoUsuario TipoUsuario { get; set; }
        public ICollection<Presenca> Presenca { get; set; }
    }
}
