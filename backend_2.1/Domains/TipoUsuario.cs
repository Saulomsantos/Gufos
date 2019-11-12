using System.Collections.Generic;

namespace senai_2s2019_CodeXP_Gufos.Domains
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int TipoUsuarioId { get; set; }
        public string Titulo { get; set; }

        public ICollection<Usuario> Usuario { get; set; }
    }
}
