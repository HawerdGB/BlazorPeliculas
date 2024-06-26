using System.ComponentModel.DataAnnotations;

namespace BlazorPeliculas.Shared.Entidades{
public class Genero
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es requerido")]
    [MinLength(3, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres")]
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }

    // Propiedad de navegación para la relación con la entidad Pelicula
    // public List<Pelicula>? Peliculas { get; set; }
}
}