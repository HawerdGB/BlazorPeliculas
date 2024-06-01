namespace BlazorPeliculas.Shared.Entidades{
public class Pelicula
{
    public int Id { get; set; }
    public string Titulo { get; set; }=null!;
     public string? Poster { get; set; }
    public string? Genero { get; set; }
    public string? Director { get; set; }
    public int Duracion { get; set; }
    public DateTime FechaLanzamiento { get; set; }
    public string? Pais { get; set; }
    public string? Descripcion { get; set; }
    public string? TituloCortado 
        { 
            get {
        if (string.IsNullOrWhiteSpace(Titulo)){
            return null;
        }
        if(Titulo.Length > 60){
            return Titulo.Substring(0, 60) + "...";
        }
        else{ return Titulo;}
    }}
}
}