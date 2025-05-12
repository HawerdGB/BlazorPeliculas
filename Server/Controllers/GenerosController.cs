using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenerosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await _context.Generos.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if (genero is null)
            {
                return NotFound();
            }
            return genero;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Genero genero)
        {

            _context.Add(genero);
            await _context.SaveChangesAsync();
            return genero.Id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Genero genero)
        {
            _context.Update(genero);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var filasAfectadas = await _context.Generos.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (filasAfectadas == 0)
            {
                return NotFound();
            }
           return NoContent();

        }
    }
}
