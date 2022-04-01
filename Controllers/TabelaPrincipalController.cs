using AngularCBFBackEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND.Controllers;

[ApiController]
[Route("[controller]")]
public class TabelaPrincipalController : ControllerBase
{
    private readonly ApplicationContextDB _context;

    public TabelaPrincipalController(ApplicationContextDB applicationContextDB){
        _context = applicationContextDB;
    }

    [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            string nome;
            int vitoria = 0, derrota = 0, empate = 0, pontos = 0;
            List<TabelaPrincipalModel> TabelaLista = new List<TabelaPrincipalModel>();
            try
            {
                

                var result = await _context.serieA
                                .Where(x => x.DataHoraJogo.Year == 2020)
                                .Select(s => new { s.NomeTimeCasa, s.PlacarTimeCasa, s.PlacarTimeVisitante })
                                .ToListAsync();

                foreach (var i in result)
                {
                    nome = i.NomeTimeCasa;
                    if (i.PlacarTimeCasa > i.PlacarTimeVisitante)
                    {
                        vitoria++;
                        pontos += 3;
                    }
                    else if (i.PlacarTimeCasa < i.PlacarTimeVisitante)
                    {
                        derrota++;
                    }
                    else
                    {
                        empate++;
                        pontos += 1;
                    }
                    TabelaLista.Add(new TabelaPrincipalModel(nome, vitoria, derrota, empate, pontos));
                }

                return Ok(TabelaLista.OrderByDescending(o => o.pontos));
            }
            catch (Exception)
            {

                throw;
            }
        }
}
