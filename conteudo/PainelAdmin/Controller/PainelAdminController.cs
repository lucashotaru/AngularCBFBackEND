using AngularCBFBackEND.conteudo.PainelAdmin.Factory;
using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AngularCBFBackEND.conteudo.PainelAdmin.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PainelAdminController : ControllerBase
    {


        private readonly ApplicationDbContext _context;

        public PainelAdminController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet("criar-excel-jogos-cbf/{tipo}/{serie}")]
        public async Task<FileContentResult> GetExcelCBF(string tipo, string serie)
        {
            var arquivoExcel = await PainelAdminFactory.GetExcelCBF(tipo, serie);

            if (arquivoExcel != null)
                return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"TabelasJogos/{serie}/{DateTime.Now.ToShortDateString()}.xlsx");

            return null;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var result = await LeitorExcelCBFRepository.LerStreamEConverterEmMemory(file);
            var times = await LeitorExcelCBFRepository.LeitorExcel(result);

            bool resultado = true;

            var serieARemover = times.Select(p => p.Serie).FirstOrDefault();

            try
            {
                _context.RemoveRange(_context.jogos.Where(rem => rem.Serie == serieARemover));

                _context.jogos.AddRange(times);

                _context.SaveChanges();

                resultado = true;
            }
            catch (System.Exception)
            {
                resultado = false;
            }

            if (resultado != false)
                return StatusCode(StatusCodes.Status201Created, new RetornoAPI { Status = "Sucesso", Message = "Excell upado com sucesso" });

            return StatusCode(StatusCodes.Status409Conflict, new RetornoAPI { Status = "Erro", Message = "Não foi possivel upar o excel" });
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> Download()
        {
            return  Ok();
        }
    }
}