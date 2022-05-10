using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;
using HtmlAgilityPack;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Factory
{
    public class PainelAdminFactory
    {
        public static async Task<Byte[]> GetExcelCBF(string tipo, string serie)
        {
            List<JogosModel> Lista = new List<JogosModel>();

            int[] dataConvertida = new int[2];
            dataConvertida = await WebScrapingJogosRepository.verificaData(tipo, serie);

            bool continuar = true;

            for (int ano = dataConvertida[0]; ano < dataConvertida[1]; ano++)
            {       
                int RodadaCount = 1, urlAtual = 0;

                string url = $"https://www.cbf.com.br/futebol-brasileiro/competicoes/{tipo}-{serie}/{ano}";

                var urlVerificada = await WebScrapingJogosRepository.ValidaFaseDaSerie(url);
                
                do
                {
                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument doc = web.Load(urlVerificada[urlAtual]);

                    int idVar = 0;;

                    if(urlVerificada.Count() > 0)
                    {
                        doc = web.Load(urlVerificada[urlAtual]);
                        continuar = true;
                    }
                    
                    if(urlAtual == urlVerificada.Count() - 1)
                        continuar = false;

                    urlAtual++;

                    try
                    {
                        var cbfNomeTimeCasaNode = doc.DocumentNode.SelectNodes("//div[@class='time pull-left']/img");
                        var cbfNomeTimeVisitanteNode = doc.DocumentNode.SelectNodes("//div[@class='time pull-right']/img");
                        var DataHoraNode = doc.DocumentNode.SelectNodes("//span[@class='partida-desc text-1 color-lightgray p-b-15 block uppercase text-center']");
                        var PlacarNode = doc.DocumentNode.SelectNodes("//div[@class='clearfix']/a/strong[@class='partida-horario center-block']");
                        var RodadaNove = doc.DocumentNode.SelectNodes("//h3[@class='text-center']");
                        var siglaTimeCasaNode = doc.DocumentNode.SelectNodes("//div[@class='time pull-left']/span");
                        var siglaTimeVisitanteNode = doc.DocumentNode.SelectNodes("//div[@class='time pull-right']/span");

                        string[] cbfNomeTimeCasa = new string[cbfNomeTimeCasaNode.Count];
                        string[] cbfNomeTimeVisitante = new string[cbfNomeTimeVisitanteNode.Count];
                        string[] Placar = new string[PlacarNode.Count];
                        int[] Rodada = new int[cbfNomeTimeCasaNode.Count];
                        string[] Data = new string[DataHoraNode.Count];
                        string[] siglaTimeCasa = new string[siglaTimeCasaNode.Count];
                        string[] siglaTimeVisitante = new string[siglaTimeVisitanteNode.Count];

                        foreach (var item in cbfNomeTimeCasaNode)
                        {
                            cbfNomeTimeCasa[idVar] = cbfNomeTimeCasaNode[idVar].GetAttributeValue("title", string.Empty).ToString();
                            cbfNomeTimeVisitante[idVar] = cbfNomeTimeVisitanteNode[idVar].GetAttributeValue("title", string.Empty).ToString();
                            Placar[idVar] = PlacarNode[idVar].InnerText.ToString().Trim();
                            Data[idVar] = DataHoraNode[idVar].InnerText.ToString().Trim().Remove(0, 5);
                            siglaTimeCasa[idVar] = siglaTimeCasaNode[idVar].InnerText;
                            siglaTimeVisitante[idVar] = siglaTimeVisitanteNode[idVar].InnerText;

                            int[] placarConvertido = new int[2];

                            placarConvertido = WebScrapingJogosRepository.FormatarPlacar(Placar[idVar]);

                            DateTime DataConvertida = WebScrapingJogosRepository.FormataData(Data[idVar]);

                            Rodada[idVar] = RodadaCount;

                            var result = new JogosModel(cbfNomeTimeCasa[idVar],
                                                        placarConvertido[0],
                                                        cbfNomeTimeVisitante[idVar],
                                                        placarConvertido[1],
                                                        Rodada[idVar],
                                                        DataConvertida,
                                                        serie,
                                                        ano,
                                                        siglaTimeCasa[idVar],
                                                        siglaTimeVisitante[idVar]
                                                        );
                                                        
                            Lista.Add(result);
                            
                            idVar++;
                        }
                        Console.WriteLine("Leitura :" + ano);
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }


                } while (continuar == true);
            }
        
            var GetArquivoExcel = await WebScrapingJogosRepository.ConverterListarCBFEmExcel(Lista);

            return GetArquivoExcel;
        } 
    }
}