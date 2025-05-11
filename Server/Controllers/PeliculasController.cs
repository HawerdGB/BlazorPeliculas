using AutoMapper;
using BlazorPeliculas.Server.Helpers;
using BlazorPeliculas.Shared.DTOs;
using BlazorPeliculas.Shared.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAlmacenadorArchivos _almacenadorArchivos;
        private readonly IMapper _mapper;
        private readonly string contenedor = "peliculas";

        public PeliculasController(ApplicationDbContext context, IAlmacenadorArchivos almacenadorArchivos,IMapper mapper)
        {
            _context = context;
            _almacenadorArchivos = almacenadorArchivos;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<HomePageDTO>> Get()
        {
            var limite = 6;

            var peliculasEnCartelera = await _context.Peliculas
                .Where(x => x.EnCartelera)
                .Take(limite)
                .OrderByDescending(x => x.Lanzamiento)
                .ToListAsync();

            var proximosEstrenos = await _context.Peliculas
                .Where(x => x.Lanzamiento > DateTime.Today)
                .OrderBy(x => x.Lanzamiento)
                .Take(limite)
                .ToListAsync();

            var resultado = new HomePageDTO
            {
                PeliculasEnCartelera = peliculasEnCartelera,
                ProximosEstrenos = proximosEstrenos
            };

            return resultado;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaVisualizarDTO>> Get(int id)
        {
            var pelicula = await _context.Peliculas.Where(x => x.Id == id)
                .Include(x => x.GenerosPelicula).ThenInclude(g => g.Genero)
                .Include(x => x.PeliculasActor.OrderBy(x => x.Orden)).ThenInclude(a => a.Actor).FirstOrDefaultAsync();

            if (pelicula == null) { return NotFound(); }


            //TODO : Sistema Votacion

            var promedioVoto = 4.0;
            var votoUsuario = 5;

            var modelo = new PeliculaVisualizarDTO();
            modelo.Pelicula = pelicula;
            modelo.Generos = pelicula.GenerosPelicula.Select(x => x.Genero).ToList()!;
            modelo.Actores = pelicula.PeliculasActor.Select(x => new Actor
            {
                Nombre = x.Actor!.Nombre,
                Personaje = x.Personaje,
                Foto = x.Actor.Foto,
                Id = x.ActorId
            }).ToList();


            modelo.VotoUsuario = votoUsuario;
            modelo.PromedioVotos = promedioVoto;

            return modelo;
        }

        [HttpGet("actualizar/{id:int}")]
        public async Task<ActionResult<PeliculaActualizacionDTO>> PutGet(int id)
        {
            var peliculaActionResult = await Get(id);
            if (peliculaActionResult.Result is NotFoundResult)
            {
                return NotFound();
            }

            var peliculaVisualizarDTO = peliculaActionResult.Value;
            var generosSeleccionadosIds = peliculaVisualizarDTO!.Generos.Select(x => x.Id).ToList();
            var generosNoSeleccionados = await _context.Generos.Where(x => !generosSeleccionadosIds.Contains(x.Id)).ToListAsync();

            var modelo = new PeliculaActualizacionDTO();
            modelo.Pelicula = peliculaVisualizarDTO.Pelicula;
            modelo.GenerosSeleccionados = peliculaVisualizarDTO.Generos;
            modelo.GenerosNoSeleccionados = generosNoSeleccionados;
            modelo.Actores = peliculaVisualizarDTO.Actores;

            return modelo;

        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Pelicula pelicula)
        {
            if (!string.IsNullOrWhiteSpace(pelicula.Poster))
            {
                var posterPelicula = Convert.FromBase64String(pelicula.Poster);
                pelicula.Poster = await _almacenadorArchivos.GuardarArchivo(posterPelicula, "jpg", contenedor);
            }

            EscribirOrdenActores(pelicula);

            _context.Add(pelicula);
            await _context.SaveChangesAsync();
            return pelicula.Id;
        }

        private static void EscribirOrdenActores(Pelicula pelicula)
        {
            if (pelicula.PeliculasActor is not null)
            {
                for (int i = 0; i < pelicula.PeliculasActor.Count; i++)
                {
                    pelicula.PeliculasActor[i].Orden = i + 1;
                }
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(Pelicula pelicula)
        {
            var peliculaDB = await _context.Peliculas
                .Include(x => x.PeliculasActor)
                .Include(x => x.GenerosPelicula)
                .FirstOrDefaultAsync(x => x.Id == pelicula.Id);
            if (peliculaDB == null) { return NotFound(); }

            peliculaDB = _mapper.Map(pelicula, peliculaDB);

            if (!string.IsNullOrWhiteSpace(pelicula.Poster))
            {
                var posterPelicula = Convert.FromBase64String(pelicula.Poster);
                peliculaDB.Poster = await _almacenadorArchivos.EditarArchivo(posterPelicula, "jpg", contenedor, peliculaDB.Poster!);
            }

            EscribirOrdenActores(peliculaDB);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
