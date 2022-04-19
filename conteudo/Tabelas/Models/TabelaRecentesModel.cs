namespace AngularCBFBackEND.conteudo.Tabelas.Models
{
    public class TabelaRecentesModel
    {
        public TabelaRecentesModel(string nomeTimeCasa, int placarTimeCasa, string nomeTimeVisitante, int placarTimeVisitante)
        {
            NomeTimeCasa = nomeTimeCasa;
            PlacarTimeCasa = placarTimeCasa;
            NomeTimeVisitante = nomeTimeVisitante;
            PlacarTimeVisitante = placarTimeVisitante;
        }

        public string NomeTimeCasa { get; set; }
        public int PlacarTimeCasa { get; set; }
        public string NomeTimeVisitante { get; set; }
        public int PlacarTimeVisitante { get; set; }
    }
}