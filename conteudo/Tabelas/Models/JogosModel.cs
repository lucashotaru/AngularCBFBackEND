namespace AngularCBFBackEND.conteudo.Tabelas.Models
{
    public class JogosModel
    {
        public int Id { get; set; }
        public string NomeTimeCasa { get; set; }
        public int PlacarTimeCasa { get; set; }
        public string NomeTimeVisitante { get; set; }
        public int PlacarTimeVisitante { get; set; }
        public int Rodada { get; set; }
        public DateTime DataHoraJogo { get; set; }
    }
}