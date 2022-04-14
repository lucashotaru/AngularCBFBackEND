using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Factory
{
    public class LeitorExcelCBFFactory
    {
        public static async Task<bool> GetExcelCBF(string tipo, string serie)
        {
            var resul1 = await WebScrapingJogosRepository.GetCBFInfo(tipo, serie);
            var resul2 = await WebScrapingJogosRepository.SaveCBFInfo(resul1);
            
            return resul2;
        } 
    }
}