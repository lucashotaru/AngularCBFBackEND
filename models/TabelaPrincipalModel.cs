namespace AngularCBFBackEND.Models
{
    public class TabelaPrincipalModel
    {
        public string nome { get; set; }
        public int vitorias { get; set; }
        public int derrotas { get; set; }
        public int empates { get; set; }
        public int pontos { get; set; }


        public TabelaPrincipalModel(string nome, int vitorias, int derrotas, int empates, int pontos)
        {
            this.nome = nome;
            this.vitorias = vitorias;
            this.derrotas = derrotas;
            this.empates = empates;
            this.pontos = pontos;
        }
    }
}