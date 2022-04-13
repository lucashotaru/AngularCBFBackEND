namespace AngularCBFBackEND.conteudo.PainelAdmin.Models
{
    public class JogosModel
    {
         public string NomeTimeCasa { get; set; }
        public int PlacarTimeCasa { get; set; }
        public string NomeTimeVisitante { get; set; }
        public int PlacarTimeVisitante { get; set; }
        public int Rodada { get; set; }
        public DateTime DataHoraJogo { get; set; }
        public string Serie {get; set;}


        public JogosModel(string TimeCasa,int PlacarCasa, string TimeVisitante, int PlacaVisitante, int rodada, DateTime dataHora, string serie)
        {
            NomeTimeCasa= TimeCasa;
            PlacarTimeCasa = PlacarCasa;
            NomeTimeVisitante = TimeVisitante;
            PlacarTimeVisitante = PlacaVisitante;
            Rodada = rodada;
            DataHoraJogo = dataHora;
            Serie = serie;
        }

        public JogosModel()
        {
        }
    }
}