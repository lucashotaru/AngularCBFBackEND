using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularCBFBackEND.conteudo.Tabelas.Models
{
    public class TabelaDataModel
    {

        public TabelaDataModel(DateTime dataAtualizacaTabela)
        {
            this.dataAtualizacaTabela = dataAtualizacaTabela;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime dataAtualizacaTabela { get; set; }    
    }
}