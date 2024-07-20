using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPeliculas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly string contenedor = "personas";

        public ActoresController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos)
        {
            _context = context;
            _almacenadorArchivos = almacenadorArchivos;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Actor actor)
        {
            if (!string.IsNullOrWhiteSpace(actor.Foto))
            {
                var fotoActor = Convert.FromBase64String(actor.Foto);
                actor.Foto = await _almacenadorArchivos.GuardarArchivo(fotoActor, "jpg", contenedor);
            }
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return actor.Id;
        }
    }
}
