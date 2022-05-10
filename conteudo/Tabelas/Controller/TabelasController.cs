using System.Collections.Generic;
using AngularCBFBackEND.conteudo.Tabelas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularCBFBackEND.conteudo.PainelAdmin.Models;


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
            string sigla;

            List<TabelasPrincipalModel> Lista = new List<TabelasPrincipalModel>();
            List<TabelasPrincipalModel> TabelaLista = new List<TabelasPrincipalModel>();

            try
            {
                var tabelaTimeCasa = await _context.jogos
                                .Where(x => x.AnoTabela == ano && x.Serie == serie)
                                .Select(s => new { s.NomeTimeCasa, s.PlacarTimeCasa, s.PlacarTimeVisitante, s.SiglaTimeCasa })
                                .ToListAsync();

                var tabelaTimeVisitante = await _context.jogos
                                .Where(x => x.AnoTabela == ano && x.Serie == serie)
                                .Select(s => new { s.NomeTimeVisitante, s.PlacarTimeVisitante, s.PlacarTimeCasa, s.SiglaTimeVisitante})
                                .ToListAsync();

                foreach (var i in tabelaTimeCasa)
                {
                    int vitoria = 0, derrota = 0, empate = 0, pontos = 0;
                    nome = i.NomeTimeCasa;
                    sigla = i.SiglaTimeCasa;

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
                    Lista.Add(new TabelasPrincipalModel(0, nome, vitoria, derrota, empate, pontos, sigla));
                }

                foreach (var v in tabelaTimeVisitante)
                {
                    int vitoria = 0, derrota = 0, empate = 0, pontos = 0;
                    nome = v.NomeTimeVisitante;
                    sigla = v.SiglaTimeVisitante;

                    if (v.PlacarTimeVisitante > v.PlacarTimeCasa)
                    {
                        vitoria = 1;
                        pontos = 3;
                    }
                    else if (v.PlacarTimeVisitante < v.PlacarTimeCasa)
                    {
                        derrota = 1;
                    }
                    else
                    {
                        empate = 1;
                        pontos += 1;
                    }
                    Lista.Add(new TabelasPrincipalModel(0 ,nome, vitoria, derrota, empate, pontos, sigla));
                }

                var nomes = Lista.Select(s => new { s.nome, s.SiglaTime}).Distinct();
                foreach (var item in nomes)
                {
                    var resultado = Lista
                            .Where(w => w.nome == item.nome)
                            .GroupBy(ud => ud.nome)
                            .Select(g => new
                            {
                                vitorias = g.Sum(c => c.vitorias),
                                derrotas = g.Sum(c => c.derrotas),
                                empates = g.Sum(c => c.empates),
                                pontos = g.Sum(c => c.pontos),
                            }).ToList(); ;
                    foreach (var item2 in resultado)
                    {
                        TabelaLista.Add(new TabelasPrincipalModel(0 , item.nome, item2.vitorias, item2.derrotas, item2.empates, item2.pontos, item.SiglaTime));
                    }
                }

                int posicao = 1;

                Lista.Clear();
                foreach (var item in TabelaLista.OrderByDescending(x => x.pontos))
                {
                   Lista.Add(new TabelasPrincipalModel(posicao++, item.nome, item.vitorias, item.derrotas, item.empates, item.pontos, item.SiglaTime));
                }

                return Ok(Lista);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [Route("tabela-jogos-recentes")]
        public async Task<IActionResult> TabelaJogosRecentes()
        {
            List<TabelaRecentesModel> ListaRecentes = new List<TabelaRecentesModel>();

            try
            {
                var TabelaJogosRecentes = await _context.jogos
                                .Select(s => new { s.SiglaTimeCasa, s.SiglaTimeVisitante, s.PlacarTimeCasa, s.PlacarTimeVisitante, s.DataHoraJogo })
                                .OrderByDescending(d => d.DataHoraJogo).Take(3)
                                .ToListAsync();

                foreach (var item in TabelaJogosRecentes)
                {
                    ListaRecentes.Add(new TabelaRecentesModel(item.SiglaTimeCasa, item.PlacarTimeCasa, item.SiglaTimeVisitante, item.PlacarTimeVisitante));
                }

                return Ok(ListaRecentes);

            }
            catch (System.Exception)
            {
                
                throw;
            }

        }    
    }
}