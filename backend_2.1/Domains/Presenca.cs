namespace senai_2s2019_CodeXP_Gufos.Domains
{
    public partial class Presenca
    {
        public int PresencaId { get; set; }
        public int? EventoId { get; set; }
        public int? UsuarioId { get; set; }
        public string Status { get; set; }

        public Evento Evento { get; set; }
        public Usuario Usuario { get; set; }
    }
}
