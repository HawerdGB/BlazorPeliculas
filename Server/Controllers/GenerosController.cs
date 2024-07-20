using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPeliculas.Server.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GenerosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Genero genero)
        {
          
            _context.Add(genero);
            await _context.SaveChangesAsync();
            return genero.Id;
        }

    }
}
