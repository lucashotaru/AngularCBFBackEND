using AngularCBFBackEND.conteudo.PainelAdmin.Factory;
using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PainelAdminController: ControllerBase
    {
        [HttpGet("criar-excel-jogos-cbf/{tipo}/{serie}")]
        public async Task<FileContentResult> GetExcelCBF(string tipo, string serie)
        {
            var filePath = await LeitorExcelCBFFactory.GetExcelCBF(tipo, serie);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("TabelaJogos");
                var linha = 1;
                worksheet.Cell(linha, 1).Value = "NomeTimeCasa";
                worksheet.Cell(linha, 2).Value = "PlacarTimeCasa";
                worksheet.Cell(linha, 3).Value = "NomeTimeVisitante";
                worksheet.Cell(linha, 4).Value = "PlacarTimeVisitante";
                worksheet.Cell(linha, 5).Value = "Rodada";
                worksheet.Cell(linha, 6).Value = "DataHoraJogo";
                worksheet.Cell(linha, 7).Value = "Serie";
                foreach (var user in filePath)
                {
                    linha++;
                    worksheet.Cell(linha, 1).Value = user.NomeTimeCasa;
                    worksheet.Cell(linha, 2).Value = user.PlacarTimeCasa;
                    worksheet.Cell(linha, 3).Value = user.NomeTimeVisitante;
                    worksheet.Cell(linha, 4).Value = user.PlacarTimeVisitante;
                    worksheet.Cell(linha, 5).Value = user.Rodada;
                    worksheet.Cell(linha, 6).Value = user.DataHoraJogo;
                    worksheet.Cell(linha, 7).Value = user.Serie;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"TabelasJogos{DateTime.Now.ToShortDateString()}.xlsx");
                }
            }
        }

        [HttpPost("importar-jogos-cbf")]
        public async Task<IActionResult> PostExcelCBF(IFormFile cbfInfo)
        {
            var result = await LeitorExcelCBFRepository.LerStreamEConverterEmMemory(cbfInfo);
            var times = await LeitorExcelCBFRepository.LeitorExcel(result);

            var ok = await LeitorExcelCBFRepository.SalvaJogosBanco(times);

            if(ok)
                return Ok();                

            return Ok();
        }
    }
}