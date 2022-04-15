using System.Collections.Generic;
using AngularCBFBackEND.conteudo.PainelAdmin.Controller;
using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using OfficeOpenXml;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Repositories
{

    public class LeitorExcelCBFRepository
    {
        public static async Task<List<JogosModel>> LeitorExcel(Stream stream)
        {
            var resultado = new List<JogosModel>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet excelWorksheet = package.Workbook.Worksheets[0];
                
                int colCount = excelWorksheet.Dimension.End.Column;

                int rowCount = excelWorksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    var jogo = new JogosModel();
                    jogo.NomeTimeCasa = excelWorksheet.Cells[row, 1].Value.ToString();
                    jogo.PlacarTimeCasa = Convert.ToInt32(excelWorksheet.Cells[row, 2].Value);
                    jogo.NomeTimeVisitante = excelWorksheet.Cells[row, 3].Value.ToString();
                    jogo.PlacarTimeVisitante = Convert.ToInt32(excelWorksheet.Cells[row, 4].Value);
                    jogo.Rodada = Convert.ToInt32(excelWorksheet.Cells[row, 5].Value);
                    jogo.DataHoraJogo = Convert.ToDateTime(excelWorksheet.Cells[row, 6].Value);
                    jogo.Serie = excelWorksheet.Cells[row, 7].Value.ToString();
                    jogo.AnoTabela = Convert.ToInt32(excelWorksheet.Cells[row, 8].Value);

                    resultado.Add(jogo);
                }
            }

            return resultado;
        }

        public static async Task<Stream> LerStreamEConverterEmMemory(IFormFile formFile)
        {
            using (var stream = new MemoryStream())
            {
                formFile?.CopyTo(stream);

                var byteArray = stream.ToArray();

                return new MemoryStream(byteArray);
            }
        }
    } 
}