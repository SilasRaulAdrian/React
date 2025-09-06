using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartiController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;

        public CartiController(BibliotecaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Carte>>> GetCarte()
        {
            var carti = await _context.Carti.ToListAsync();
            return Ok(carti);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carte>> GetCarteById(int id)
        {
            var carte = await _context.Carti.FirstOrDefaultAsync(c => c.CarteID == id);

            if (carte == null)
                return NotFound("Cartea nu a fost gasita");

            return Ok(carte);
        }

        [HttpPost]
        public async Task<ActionResult<Carte>> PostCarte(Carte carte)
        {
            _context.Carti.Add(carte);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Carte>> PutCarte(int id, Carte carte)
        {
            if (id != carte.CarteID)
                return BadRequest();

            var car = _context.Carti.FirstOrDefault(c => c.CarteID == id);

            if (car == null)
                return NotFound();

            car.Titlu = carte.Titlu;
            car.Autor = carte.Autor;
            car.DataPub = carte.DataPub;
            car.Volum = carte.Volum;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<Carte>>> GetCarteByTitlu([FromQuery] string text, [FromQuery] string sortBy)
        {
            var carti = await _context.Carti.ToListAsync();
            var car = carti.Where(c => c.Titlu.StartsWith(text, StringComparison.OrdinalIgnoreCase));

            switch (sortBy)
            {
                case "titlu":
                    car = car.OrderBy(c => c.Titlu);
                    break;
                case "autor":
                    car = car.OrderByDescending(c => c.Autor);
                    break;
                case "data":
                    car = car.OrderByDescending(c => c.DataPub);
                    break;
                case "volum":
                    car = car.OrderBy(c => c.Volum);
                    break;
                default:
                    return BadRequest();
            }

            return Ok(car);
        }

        [HttpDelete]
        public async Task<ActionResult<Carte>> DeleteCarte([FromQuery] int id)
        {
            var carte = await _context.Carti.FirstOrDefaultAsync(c => c.CarteID == id);

            if (carte == null)
                return NotFound();

            _context.Carti.Remove(carte);
            await _context.SaveChangesAsync();
            return Ok(carte);
        }
    }
}
