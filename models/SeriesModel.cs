using AngularCBFBackEND.Models;
namespace AngularCBFBackEND.models

{
    public class SeriesModel
    {
        public string NomeTimeCasa { get; set; }
        public int PlacarTimeCasa { get; set; }
        public string NomeTimeVisitante { get; set; }
        public int PlacarTimeVisitante { get; set; }
        public int Rodada { get; set; }
        public DateTime DataHoraJogo { get; set; }
    }
}