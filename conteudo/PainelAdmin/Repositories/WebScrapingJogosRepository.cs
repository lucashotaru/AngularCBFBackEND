using System.Globalization;
using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using ClosedXML.Excel;
using HtmlAgilityPack;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Repositories
{
    public class WebScrapingJogosRepository
    {
        public static async Task<List<JogosModel>> GetCBFInfo(string tipo, string serie)
        {
            List<JogosModel> Lista = new List<JogosModel>();

            DateTime dataAgora = DateTime.Now;
            int dataConvertida = dataAgora.Year;

            for (int y = 2012; y < dataConvertida; y++)
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load($"https://www.cbf.com.br/futebol-brasileiro/competicoes/{tipo}-{serie}/{y}");

                try
                {
                    var cbfNomeTimeCasaNode = doc.DocumentNode.SelectNodes("//div[@class='time pull-left']/img");
                    var cbfNomeTimeVisitanteNode = doc.DocumentNode.SelectNodes("//div[@class='time pull-right']/img");
                    var DataHoraNode = doc.DocumentNode.SelectNodes("//span[@class='partida-desc text-1 color-lightgray p-b-15 block uppercase text-center']");
                    var PlacarNode = doc.DocumentNode.SelectNodes("//div[@class='clearfix']/a/strong[@class='partida-horario center-block']");

                    int i = 0, r = 10, rr = 1;
                    string[] cbfNomeTimeCasa = new string[cbfNomeTimeCasaNode.Count];
                    string[] cbfNomeTimeVisitante = new string[cbfNomeTimeVisitanteNode.Count];
                    string[] PlacarCasa = new string[PlacarNode.Count];
                    string[] PlacarVisitante = new string[PlacarNode.Count];
                    int[] Rodada = new int[cbfNomeTimeCasaNode.Count];
                    string[] Data = new string[DataHoraNode.Count];
                    DateTime[] Date = new DateTime[DataHoraNode.Count];
                    Rodada[0] = 0;


                    foreach (var item in cbfNomeTimeCasaNode)
                    {
                        cbfNomeTimeCasa[i] = cbfNomeTimeCasaNode[i].GetAttributeValue("alt", string.Empty).ToString();

                        cbfNomeTimeVisitante[i] = cbfNomeTimeVisitanteNode[i].GetAttributeValue("alt", string.Empty).ToString();

                        PlacarCasa[i] = PlacarNode[i].InnerText.ToString().Trim();
                        PlacarVisitante[i] = PlacarNode[i].InnerText.ToString().Trim();

                        PlacarCasa[i] = PlacarCasa[i].Remove(PlacarCasa[i].Length - 4);
                        if (PlacarCasa[i].Length > 1)
                        {
                            PlacarCasa[i] = PlacarCasa[i].Remove(0, 3);
                        }
                        PlacarVisitante[i] = PlacarVisitante[i].Remove(0, 4);
                        if (PlacarVisitante[i].Length > 1)
                        {
                            PlacarVisitante[i] = PlacarVisitante[i].Remove(0, 2);
                        }

                        Data[i] = DataHoraNode[i].InnerText.ToString().Trim();
                        Data[i] = Data[i].Remove(0, 5);
                        while (Data[i].Length > 16)
                        {
                            Data[i] = Data[i].Remove(Data[i].Length - 1);
                        }

                        if (i + 1 > r)
                        {
                            rr++;
                            r += 10;
                        }
                        Rodada[i] = rr;

                        DateTime data = DateTime.ParseExact(Data[i], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                        
                            var result = new JogosModel(cbfNomeTimeCasa[i],
                                                        Convert.ToInt32(PlacarCasa[i]),
                                                        cbfNomeTimeVisitante[i],
                                                        Convert.ToInt32(PlacarVisitante[i]),
                                                        Rodada[i],
                                                        data,
                                                        serie,
                                                        y);
                            try
                            {

                                Lista.Add(result);
                            }
                            catch (Exception ex)
                            {

                                Console.WriteLine(ex);
                            }

                        i++;
                    }
                    Console.WriteLine("Leitura :" + y);
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return Lista;
        }

        public static async Task<Byte[]> ConverterListarCBFEmExcel(List<JogosModel> lista)
        {
            
            return await Task.Run(async ()=>{

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
                    worksheet.Cell(linha, 7).Value = "Serie";

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
                    }
                );
            
        }
    }
}