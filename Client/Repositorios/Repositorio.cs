using BlazorPeliculas.Shared.Entidades;
using System.Text;
using System.Text.Json;


namespace BlazorPeliculas.Client.Repositorios
{
    public class Repositorio : IRepositorio
    {
        private readonly HttpClient httpClient;

        public Repositorio(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await httpClient.PostAsync(url, enviarContent);

            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public List<Pelicula> ObtenerPeliculas()
        {
            return new List<Pelicula>
            {
                new Pelicula { Titulo = "Superman", Lanzamiento = new DateTime(2022, 07, 15), Poster = "https://i.redd.it/o472l59qjtz71.jpg" },
                new Pelicula { Titulo = "El Señor de los Anillos", Lanzamiento = new DateTime(2019, 03, 25), Poster = "https://m.media-amazon.com/images/I/51Qvs9i5a%2BL._AC_SY679_.jpg" },
                new Pelicula { Titulo = "Avengers", Lanzamiento = new DateTime(2023, 04, 03), Poster = "https://m.media-amazon.com/images/I/81ExhpBEbHL._AC_SY679_.jpg" },
                new Pelicula { Titulo = "Inception", Lanzamiento = new DateTime(2010, 07, 16), Poster = "https://m.media-amazon.com/images/I/81p+xe8cbnL._AC_SY679_.jpg" },
                new Pelicula { Titulo = "Matrix", Lanzamiento = new DateTime(1999, 03, 31), Poster = "https://m.media-amazon.com/images/I/51vpnbwFHrL._AC_SY679_.jpg" },
                new Pelicula { Titulo = "Interstellar", Lanzamiento = new DateTime(2014, 11, 07), Poster = "https://m.media-amazon.com/images/I/91kFYg4fX3L._AC_SY679_.jpg" },
                new Pelicula { Titulo = "The Dark Knight", Lanzamiento = new DateTime(2008, 07, 18), Poster = "https://m.media-amazon.com/images/I/818hyvdVfvL._AC_UF894,1000_QL80_.jpg" },
                new Pelicula { Titulo = "Pulp Fiction", Lanzamiento = new DateTime(1994, 10, 14), Poster = "https://m.media-amazon.com/images/I/71c05lTE03L._AC_SY679_.jpg" },
                new Pelicula { Titulo = "Fight Club", Lanzamiento = new DateTime(1999, 10, 15), Poster = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQjLoO7hPa_N877t7nNw-ho6C_1IGertZPJyQ&s" },
                new Pelicula { Titulo = "Forrest Gump", Lanzamiento = new DateTime(1994, 07, 06), Poster = "https://petersbilliards.com//assets/uploads/forrest-gump-movie-poster.jpg" },
                new Pelicula { Titulo = "The Shawshank Redemption", Lanzamiento = new DateTime(1994, 09, 23), Poster = "https://m.media-amazon.com/images/I/51NiGlapXlL._AC_SY679_.jpg" },
                new Pelicula { Titulo = "The Godfather", Lanzamiento = new DateTime(1972, 03, 24), Poster = "https://m.media-amazon.com/images/I/71khjV-MoOS._AC_UF894,1000_QL80_.jpg" },
                new Pelicula { Titulo = "Star Wars: A New Hope", Lanzamiento = new DateTime(1977, 05, 25), Poster = "https://static.posters.cz/image/1300/posters/star-wars-episode-iv-a-new-hope-i90218.jpg" },
                new Pelicula { Titulo = "Jurassic Park", Lanzamiento = new DateTime(1993, 06, 11), Poster = "https://i.ebayimg.com/images/g/-ggAAOSwACNgsOd~/s-l1600.jpg" },
                new Pelicula { Titulo = "The Lion King", Lanzamiento = new DateTime(1994, 06, 24), Poster = "https://i.ebayimg.com/images/g/IogAAOSwd49i-5n8/s-l1200.jpg" },
                new Pelicula { Titulo = "Back to the Future", Lanzamiento = new DateTime(1985, 07, 03), Poster = "https://cdn.europosters.eu/image/750/posters/back-to-the-future-i2795.jpg" },
                new Pelicula { Titulo = "Gladiator", Lanzamiento = new DateTime(2000, 05, 05), Poster = "https://m.media-amazon.com/images/I/61O9+6+NxYL._AC_UF894,1000_QL80_.jpg" },
                new Pelicula { Titulo = "Braveheart", Lanzamiento = new DateTime(1995, 05, 24), Poster = "https://m.media-amazon.com/images/I/51BBib1XCeL._AC_UF894,1000_QL80_.jpg" },
                new Pelicula { Titulo = "Titanic", Lanzamiento = new DateTime(1997, 12, 19), Poster = "https://m.media-amazon.com/images/I/71uoicxpqoS._AC_UF1000,1000_QL80_.jpg" },
                new Pelicula { Titulo = "Avatar", Lanzamiento = new DateTime(2009, 12, 18), Poster = "https://m.media-amazon.com/images/I/41kTVLeW1CL._AC_UF894,1000_QL80_.jpg" }
            };
        }
    }
}