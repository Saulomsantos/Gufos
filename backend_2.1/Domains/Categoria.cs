using System.Collections.Generic;
using System.ComponentModel;

namespace senai_2s2019_CodeXP_Gufos.Domains
{
    /// <summary>
    /// Classe responsável pelo modelo da categoria
    /// </summary>
    public partial class Categoria
    {
        public Categoria()
        {
            Evento = new HashSet<Evento>();
        }

        [DisplayName("ID da categoria")]
        public int CategoriaId { get; set; }

        [DisplayName("Título da categoria")]
        public string Titulo { get; set; }

        public ICollection<Evento> Evento { get; set; }
    }
}
