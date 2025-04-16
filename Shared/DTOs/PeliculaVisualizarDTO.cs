using BlazorPeliculas.Shared.Entidades;

namespace BlazorPeliculas.Shared.DTOs
{
    public class PeliculaVisualizarDTO
    {
        public Pelicula Pelicula { get; set; } = null!;
        public List<Genero> Generos { get; set; } = null!;
        public List<Actor> Actores { get; set; } = null!;
        public int VotoUsuario { get; set; }
        public double PromedioVotos { get; set; }
    }
}
