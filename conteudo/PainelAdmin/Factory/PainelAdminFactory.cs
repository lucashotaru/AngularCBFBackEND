using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Factory
{
    public class PainelAdminFactory
    {
        public static async Task<Byte[]> GetExcelCBF(string tipo, string serie)
        {
            var GetListaJogos = await WebScrapingJogosRepository.GetCBFInfo(tipo, serie);

            var GetArquivoExcel = await WebScrapingJogosRepository.ConverterListarCBFEmExcel(GetListaJogos);

            return GetArquivoExcel;
        } 
    }
}