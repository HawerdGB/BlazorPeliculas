using Microsoft.EntityFrameworkCore;

namespace BlazorPeliculas.Server.Helpers
{
    public static class HTTPContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnRespuesta<T>(this HttpContext httpContext, IQueryable<T> queryable, int cantidadRegistrosAMostrar)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            double conteo = await queryable.CountAsync();
            double totalPaginas = Math.Ceiling(conteo / cantidadRegistrosAMostrar);
            httpContext.Response.Headers.Add("conteo", conteo.ToString());
            httpContext.Response.Headers.Add("totalPaginas", totalPaginas.ToString());
        }
    }
}
