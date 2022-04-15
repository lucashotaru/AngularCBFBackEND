using AngularCBFBackEND.conteudo.Tabelas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularCBFBackEND.conteudo.Tabelas.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TabelasController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public TabelasController(ApplicationDbContext contexto)
        {
            _context = contexto;
        }
        

        [HttpGet]
        [Route("tabela-principal/{ano}/{serie}")]
        public async Task<IActionResult> TabelaPrincipal(int ano, string serie)
        {
            string nome;

            List<TabelasPrincipalModel> ListaComparacao = new List<TabelasPrincipalModel>();
            List<TabelasPrincipalModel> TabelaLista = new List<TabelasPrincipalModel>();

            try
            {
                var result = await _context.jogos
                                .Where(x => x.DataHoraJogo.Year == ano && x.Serie == serie)
                                .Select(s => new { s.NomeTimeCasa, s.PlacarTimeCasa, s.PlacarTimeVisitante })
                                .ToListAsync();

                foreach (var i in result)
                {
                    int vitoria = 0, derrota = 0, empate = 0, pontos = 0;
                    nome = i.NomeTimeCasa;
                    if (i.PlacarTimeCasa > i.PlacarTimeVisitante)
                    {
                        vitoria = 1;
                        pontos = 3;
                    }
                    else if (i.PlacarTimeCasa < i.PlacarTimeVisitante)
                    {
                        derrota = 1;
                    }
                    else
                    {
                        empate = 1;
                        pontos += 1;
                    }
                    ListaComparacao.Add(new TabelasPrincipalModel(nome, vitoria, derrota, empate, pontos));
                }

                var nomes = ListaComparacao.Select(s => s.nome).Distinct();
                foreach (var item in nomes)
                {
                    var resultado = ListaComparacao
                            .Where(w => w.nome == item)
                            .GroupBy(ud => ud.nome)
                            .Select(g => new
                            {
                                vitorias = g.Sum(c => c.vitorias),
                                derrotas = g.Sum(c => c.derrotas),
                                empates = g.Sum(c => c.empates),
                                pontos = g.Sum(c => c.pontos)
                            }).ToList(); ;
                    foreach (var item2 in resultado)
                    {
                        TabelaLista.Add(new TabelasPrincipalModel(item, item2.vitorias, item2.derrotas, item2.empates, item2.pontos));
                    }
                }

                return Ok(TabelaLista.OrderByDescending(x => x.pontos));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}