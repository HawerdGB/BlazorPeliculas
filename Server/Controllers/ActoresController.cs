using AutoMapper;
using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly IMapper _mapper;
        private readonly string contenedor = "personas";

        public ActoresController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos,IMapper mapper)
        {
            _context = context;
            _almacenadorArchivos = almacenadorArchivos;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            return await _context.Actores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> Get(int id)
        {
            var actor = await _context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actor is null)
            {
                return NotFound();
            }
            return actor;
        }


        [HttpGet("buscar/{textoBusqueda}")]
        public async Task<ActionResult<List<Actor>>> Get(string textoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(textoBusqueda)) 
            { return new List<Actor>(); }

            textoBusqueda = textoBusqueda.ToLower();

            return await _context.Actores
                      .Where(x => x.Nombre.ToLower().Contains(textoBusqueda))
                      .Take(5)
                      .ToListAsync();
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

        //crear metodo para editar actor
        [HttpPut]
        public async Task<ActionResult> Put(Actor actor)
        {
            var actorDB = await _context.Actores.FirstOrDefaultAsync(x => x.Id == actor.Id);
            if (actorDB is null)
            {
                return NotFound();
            }
             
            actorDB = _mapper.Map(actor, actorDB);

            if (!string.IsNullOrWhiteSpace(actor.Foto))
            {
                var fotoActor = Convert.FromBase64String(actor.Foto);
                actorDB.Foto = await _almacenadorArchivos.EditarArchivo(fotoActor, "jpg", contenedor, actorDB.Foto!);
            }
            else
            {
                actorDB.Foto = actor.Foto;
            }

            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
