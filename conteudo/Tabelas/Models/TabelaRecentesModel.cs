namespace AngularCBFBackEND.conteudo.Tabelas.Models
{
    public class TabelaRecentesModel
    {
        public TabelaRecentesModel(string siglaTimeCasa, int placarTimeCasa, string siglaTimeVisitante, int placarTimeVisitante)
        {
            SiglaTimeCasa = siglaTimeCasa;
            PlacarTimeCasa = placarTimeCasa;
            SiglaTimeVisitante = siglaTimeVisitante;
            PlacarTimeVisitante = placarTimeVisitante;
        }

        public string SiglaTimeCasa { get; set; }
        public int PlacarTimeCasa { get; set; }
        public string SiglaTimeVisitante { get; set; }
        public int PlacarTimeVisitante { get; set; }
    }
}