using System.Globalization;
using System.Text.RegularExpressions;
using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using ClosedXML.Excel;
using HtmlAgilityPack;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Repositories
{
    public class WebScrapingJogosRepository
    {
        public static async Task<Byte[]> ConverterListarCBFEmExcel(List<JogosModel> lista)
        {
            
            return await Task.Run(() =>
            {

                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("TabelaJogos");
                var linha = 1;

                worksheet.Cell(linha, 1).Value = "NomeTimeCasa";
                worksheet.Cell(linha, 2).Value = "PlacarTimeCasa";
                worksheet.Cell(linha, 3).Value = "NomeTimeVisitante";
                worksheet.Cell(linha, 4).Value = "PlacarTimeVisitante";
                worksheet.Cell(linha, 5).Value = "Rodada";
                worksheet.Cell(linha, 6).Value = "DataHoraJogo";
                worksheet.Cell(linha, 7).Value = "Serie";
                worksheet.Cell(linha, 8).Value = "AnoTabela";

                foreach (var tabela in lista)
                {
                    linha++;
                    worksheet.Cell(linha, 1).Value = tabela.NomeTimeCasa;
                    worksheet.Cell(linha, 2).Value = tabela.PlacarTimeCasa;
                    worksheet.Cell(linha, 3).Value = tabela.NomeTimeVisitante;
                    worksheet.Cell(linha, 4).Value = tabela.PlacarTimeVisitante;
                    worksheet.Cell(linha, 5).Value = tabela.Rodada;
                    worksheet.Cell(linha, 6).Value = tabela.DataHoraJogo;
                    worksheet.Cell(linha, 7).Value = tabela.Serie;
                    worksheet.Cell(linha, 8).Value = tabela.AnoTabela;
                }

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var content = stream.ToArray();

                return content;
            });
        }


        public static int[] FormatarPlacar(string Placar)
        {
            int[] placar = new int[2];

            var a = Regex.Replace(Placar, @"[:><'!@#$%*-+?;.,ç~{}x()\0 ]", "");

            placar[0] = Convert.ToInt32(a.Remove(a.Length - 1));
            placar[1] = Convert.ToInt32(a.Remove(0, 1));

            return placar;
        }

         public static DateTime FormataData(string data)
        {
            var Data = Regex.Replace(data, @"[><'!@#$%*-+?;.,ç~{}x()\0rnJogo]", "");

            while(Data.Length > 16)
            {
                Data = Data.Remove(Data.Length - 1);
            }

            DateTime dataConvertida = DateTime.ParseExact(Data, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            return dataConvertida;
        }

        public static async Task<string[]> ValidaFaseDaSerie(string urlfac)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(urlfac);

            int valida = 0;

            var validaUrlNode = doc.DocumentNode.SelectNodes("//select[@class='form-control big auto-select']/option");

            if(validaUrlNode == null)
            {
                string[] url = new string[1];
                url[0] = urlfac;

                return url;
            }
            else
            {
                string[] url = new string[validaUrlNode.Count];

                foreach (var item in validaUrlNode)
                {
                    url[valida] = validaUrlNode[valida].GetAttributeValue("value", string.Empty).ToString();
                    valida++;
                }

                return url;
            }

        }

        public static int[] verificaData(string tipo, string serie)
        {
            int[] dataConvertida = new int[2];

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load($"https://www.cbf.com.br/futebol-brasileiro/competicoes/{tipo}-{serie}");
            
            var dataConvertidaNode = doc.DocumentNode.SelectNodes("//select[@class='form-control auto-select']/option");

            int cont = dataConvertidaNode.Count;

            dataConvertida[1] = Convert.ToInt32(dataConvertidaNode[0].InnerHtml);
            dataConvertida[0] = Convert.ToInt32(dataConvertidaNode[cont - 1].InnerHtml);

            return dataConvertida;
        }
    }
}