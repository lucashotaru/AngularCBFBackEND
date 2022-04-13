using AngularCBFBackEND.conteudo.PainelAdmin.Factory;
using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PainelAdminController: ControllerBase
    {
        [HttpGet("criar-excel-jogos-cbf/{tipo}/{serie}")]
        public async Task<ActionResult> GetExcelCBF(string tipo, string serie)
        {
            var filePath = await LeitorExcelCBFFactory.GetExcelCBF(tipo, serie);

            return filePath;
        }

        [HttpPost("importar-jogos-cbf")]
        public async Task<IActionResult> PostExcelCBF(IFormFile cbfInfo)
        {
            var result = await LeitorExcelCBFRepository.LerStreamEConverterEmMemory(cbfInfo);
            var times = await LeitorExcelCBFRepository.LeitorExcel(result);

            var ok = await LeitorExcelCBFRepository.SalvaJogosBanco(times);

            return Ok();
        }
    }
}