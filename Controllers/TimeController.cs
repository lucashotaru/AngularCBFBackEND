using AutoMapper;
using AngularCBFBackEND.Models;
using Microsoft.AspNetCore.Mvc;
using AngularCBFBackEND.Data.Dtos.Time;
using AngularCBFBackEND.Data.Dtos;

namespace AngularCBFBackEND.Controllers
{
    [ApiController]
    [Route("estatistica/[controller]")]
    public class TimeController : ControllerBase
    {

        private ApplicationDbContext _context;
        private IMapper _mapper;

        public TimeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddTime ([FromBody] CreateTimeDto timeDto)
        {
            Time time = _mapper.Map<Time>(timeDto);

            _context.Times.Add(time);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetTimeById), new { Id = time.Id }, time);
        }

        [HttpGet]
        public IEnumerable<Time> GetTimes ()
        {
            return _context.Times;
        }

        [HttpGet("{id}")]
        public IActionResult GetTimeById (int id)
        {
            Time time = _context.Times.FirstOrDefault(time => time.Id == id);

            if (time != null)
            {
                ReadTimeDto timeDto = _mapper.Map<ReadTimeDto>(time);

                return Ok(timeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTime(int id, [FromBody] UpdateTimeDto timeDto)
        {
            Time time = _context.Times.FirstOrDefault(time => time.Id == id);

            if (time == null)
                return NotFound();

            _mapper.Map(timeDto, time);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTime(int id)
        {
            Time time = _context.Times.FirstOrDefault(time => time.Id == id);

            if (time == null)
                return NotFound();

            _context.Remove(time);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
