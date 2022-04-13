using System.ComponentModel.DataAnnotations.Schema;

namespace AngularCBFBackEND.conteudo.Tabelas.Models
{
    public class TabelasPrincipalModel
    {
        public TabelasPrincipalModel()
        {
        }

        public TabelasPrincipalModel(string nome, int vitorias, int derrotas, int empates, int pontos)
        {
            this.nome = nome;
            this.vitorias = vitorias;
            this.derrotas = derrotas;
            this.empates = empates;
            this.pontos = pontos;
        }

        public string nome { get; set; }
        public int vitorias { get; set; }
        public int derrotas { get; set; }
        public int empates { get; set; }
        public int pontos { get; set; }
    }
}