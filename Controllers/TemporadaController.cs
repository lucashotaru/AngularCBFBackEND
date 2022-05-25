using AutoMapper;
using AngularCBFBackEND.Data;
using AngularCBFBackEND.Data.Dtos.Temporada;
using AngularCBFBackEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCBFBackEND.Controllers
{
    [ApiController]
    [Route("estatistica/[controller]")]

    public class TemporadaController : ControllerBase
    {

        private ApplicationDbContext _context;
        private IMapper _mapper;

        public TemporadaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddTemporada ([FromBody] CreateTemporadaDto temporadaDto)
        {
            Temporada temporada = _mapper.Map<Temporada>(temporadaDto);

            _context.Temporadas.Add(temporada);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTemporadaById) ,new { Id = temporada.Id }, temporada);
        }

        [HttpGet]
        public IEnumerable<Temporada> GetTemporadas ()
        {
            return _context.Temporadas;
        }

        [HttpGet("{id}")]
        public IActionResult GetTemporadaById (int id)
        {
            Temporada temporada =  _context.Temporadas.FirstOrDefault(temporada => temporada.Id == id);

            if (temporada != null)
            {
                ReadTemporadaDto temporadaDto = _mapper.Map<ReadTemporadaDto>(temporada);

                return Ok(temporadaDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTemporada(int id, [FromBody] UpdateTemporadaDto temporadaDto)
        {
            Temporada temporada = _context.Temporadas.FirstOrDefault(temporada => temporada.Id == id);

            if (temporada == null)
                return NotFound();

            _mapper.Map(temporadaDto, temporada);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTemporada(int id)
        {
            Temporada temporada = _context.Temporadas.FirstOrDefault(temporada => temporada.Id == id);

            if (temporada == null)
                return NotFound();

            _context.Remove(temporada);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
