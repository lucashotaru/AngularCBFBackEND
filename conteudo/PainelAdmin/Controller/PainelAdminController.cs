using System.Drawing;
using System.Text.RegularExpressions;
using AngularCBFBackEND.conteudo.PainelAdmin.Factory;
using AngularCBFBackEND.conteudo.PainelAdmin.Models;
using AngularCBFBackEND.conteudo.PainelAdmin.Repositories;
using AngularCBFBackEND.conteudo.Tabelas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

                var data = new TabelaDataModel(DateTime.Now);

                _context.dataTabela.Add(data);

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
        [Route("getUltimaAtualizacaoTabela")]
        public async Task<IActionResult> getUltimaAtualizacaoTabela()
        {
            try
            {
                var data = await _context.dataTabela.Select(s => s.dataAtualizacaTabela).ToListAsync();

                var t = DateTime.Now - data.LastOrDefault();

                string retorno = string.Format("{0:D2} horas e {1:D2} minutos atrás",
                                t.Hours,
                                t.Minutes);


                return Ok(retorno);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getNumeroUsuarios")]
        public async Task<IActionResult> getNumeroUsuarios()
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var retorno = _context.Users.Count();
                    return Ok(retorno);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getNumeroJogos")]
        public async Task<IActionResult> getNumeroJogos()
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var retorno = _context.jogos.Count();
                    return Ok(retorno);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("getChartsJSDados")]
        public async Task<IActionResult> getChartsJSDados()
        {
            List<chartjsModel> ListaDadosChatJS = new List<chartjsModel>();
            var rnd = new Random();

            try
            {
                var a = _context.jogos.Select(s => new { s.DataHoraJogo, s.AnoTabela });

                for (int i = 2012; i < 2022; i++)
                {
                    var cont = 0;

                    var random = new Random();
                    var cores = rnd.Next(255) + ", " + rnd.Next(255) + ", " + rnd.Next(255);
                    var BackCor = "rgba("+cores+", 0.2)";
                    var cor = "rgb("+cores+")";

                    dataChartjsModel[] data = new dataChartjsModel[12];

                    for (int y = 0; y < 12; y++)
                    {
                        if (y == 0)
                            data[cont] = new dataChartjsModel("janeiro", a.Where(w => w.DataHoraJogo.Month == 2 && w.AnoTabela == i).Count());
                        else if (y == 1)
                            data[cont] = new dataChartjsModel("fevereiro", a.Where(w => w.DataHoraJogo.Month == 2 && w.AnoTabela == i).Count());
                        else if (y == 2)
                            data[cont] = new dataChartjsModel("março", a.Where(w => w.DataHoraJogo.Month == 3 && w.AnoTabela == i).Count());
                        else if (y == 3)
                            data[cont] = new dataChartjsModel("abril", a.Where(w => w.DataHoraJogo.Month == 4 && w.AnoTabela == i).Count());
                        else if (y == 4)
                            data[cont] = new dataChartjsModel("maio", a.Where(w => w.DataHoraJogo.Month == 5 && w.AnoTabela == i).Count());
                        else if (y == 5)
                            data[cont] = new dataChartjsModel("junho", a.Where(w => w.DataHoraJogo.Month == 6 && w.AnoTabela == i).Count());
                        else if (y == 6)
                            data[cont] = new dataChartjsModel("julho", a.Where(w => w.DataHoraJogo.Month == 7 && w.AnoTabela == i).Count());
                        else if (y == 7)
                            data[cont] = new dataChartjsModel("agosto", a.Where(w => w.DataHoraJogo.Month == 8 && w.AnoTabela == i).Count());
                        else if (y == 8)
                            data[cont] = new dataChartjsModel("setembro", a.Where(w => w.DataHoraJogo.Month == 9 && w.AnoTabela == i).Count());
                        else if (y == 9)
                            data[cont] = new dataChartjsModel("outubro", a.Where(w => w.DataHoraJogo.Month == 10 && w.AnoTabela == i).Count());
                        else if (y == 10)
                            data[cont] = new dataChartjsModel("novembro", a.Where(w => w.DataHoraJogo.Month == 11 && w.AnoTabela == i).Count());
                        else if (y == 11)
                            data[cont] = new dataChartjsModel("dezembro", a.Where(w => w.DataHoraJogo.Month == 12 && w.AnoTabela == i).Count());

                        cont++;
                    }
                    ListaDadosChatJS.Add(new chartjsModel(i, data, true, BackCor, cor, cor, "#fff", "#fff", cor));

                }

                return Ok(ListaDadosChatJS);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}