using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Models
{
    public class chartjsModel
    {
        public chartjsModel(int label, dataChartjsModel[] data, bool fill,string backgroundColor, string borderColor, string pointBackgroundColor, string pointBorderColor, string pointHoverBackgroundColor, string pointHoverBorderColor)
        {
            Label = label;
            Data = data;
            Fill = fill;
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
            PointBackgroundColor = pointBackgroundColor;
            PointBorderColor = pointBorderColor;
            PointHoverBackgroundColor = pointHoverBackgroundColor;
            PointHoverBorderColor = pointHoverBorderColor;
        }

        public int Label { get; set; }
        public dataChartjsModel[] Data { get; set; }
        public bool Fill { get; set; }
        public String BackgroundColor { get; set; }
        public String BorderColor { get; set; }
        public String PointBackgroundColor { get; set; }
        public String PointBorderColor { get; set; }
        public String PointHoverBackgroundColor { get; set; }
        public String PointHoverBorderColor { get; set; }
        //public String? Options { get; set; }
    }
}