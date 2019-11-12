using System;
using System.Collections.Generic;

namespace senai_2s2019_CodeXP_Gufos.Domains
{
    public partial class Evento
    {
        public Evento()
        {
            Presenca = new HashSet<Presenca>();
        }

        public int EventoId { get; set; }
        public string Titulo { get; set; }
        public int? CategoriaId { get; set; }
        public bool? AcessoLivre { get; set; }
        public DateTime DataEvento { get; set; }
        public int? LocalizacaoId { get; set; }

        public Categoria Categoria { get; set; }
        public Localizacao Localizacao { get; set; }
        public ICollection<Presenca> Presenca { get; set; }
    }
}
