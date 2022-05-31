using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Models
{
    public class dataChartjsModel
    {
        public String X { get; set; }
        public int Y { get; set; }

        public dataChartjsModel(String x, int y)
        {
            X = x;
            Y = y;
        }
    }



}
