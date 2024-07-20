
namespace BlazorPeliculas.Server.Helpers
{
    /// <summary>
    /// Clase que implementa la interfaz IAlmacenadorArchivos y se encarga de almacenar archivos localmente.
    /// </summary>
    public class AlmacenadorArchivosLocal : IAlmacenadorArchivos
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor de la clase AlmacenadorArchivosLocal.
        /// </summary>
        /// <param name="env">El entorno de alojamiento web.</param>
        /// <param name="httpContextAccessor">El accesor de contexto HTTP.</param>
        public AlmacenadorArchivosLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Elimina un archivo.
        /// </summary>
        /// <param name="ruta">La ruta del archivo a eliminar.</param>
        /// <param name="nombreContenedor">El nombre del contenedor de archivos.</param>
        /// <returns>Una tarea que representa la operación de eliminación del archivo.</returns>
        public Task EliminarArchivo(string ruta, string nombreContenedor)
        {
            var nombreArchivo = Path.GetFileName(ruta);
            var directorioArchivo = Path.Combine(_env.WebRootPath, nombreContenedor, nombreArchivo);
            if (File.Exists(directorioArchivo))
            {
                File.Delete(directorioArchivo);
            }


            return Task.CompletedTask;

        }

        /// <summary>
        /// Guarda un archivo.
        /// </summary>
        /// <param name="contenido">El contenido del archivo.</param>
        /// <param name="extension">La extensión del archivo.</param>
        /// <param name="nombreContenedor">El nombre del contenedor de archivos.</param>
        /// <returns>Una tarea que representa la operación de guardado del archivo.</returns>
        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string nombreContenedor)
        {
            var nombreArchivo = $"{Guid.NewGuid()}.{extension}";
            var folder = Path.Combine(_env.WebRootPath, nombreContenedor);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string rutaGuardado = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(rutaGuardado, contenido);

            var urlActual = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var rutaParaDB = Path.Combine(urlActual, nombreContenedor, nombreArchivo).Replace("\\", "/");

            return rutaParaDB;
        }
    }
}
