using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Factory
{
    public class LeitorExcelCBFFactory
    {
        public static async Task<List<JogosModel>> GetExcelCBF(string tipo, string serie)
        {
            var GetListaJogos = await WebScrapingJogosRepository.GetCBFInfo(tipo, serie);
                        
            return GetListaJogos;
        } 
    }
}