using System.Collections.Generic;

namespace senai_2s2019_CodeXP_Gufos.Domains
{
    public partial class Localizacao
    {
        public Localizacao()
        {
            Evento = new HashSet<Evento>();
        }

        public int LocalizacaoId { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }

        public ICollection<Evento> Evento { get; set; }
    }
}
