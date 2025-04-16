using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BlazorPeliculas.Client.Repositorios
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }
        public T? Response { get; set; }
        public bool Error { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string> ObtenerMensajeError()
        {
            if (!Error)
            { return null!; }

            var codigoEstatus = HttpResponseMessage.StatusCode;

            if (codigoEstatus == HttpStatusCode.NotFound)
            {
                return "No se encontró el recurso solicitado";
            }else if (codigoEstatus == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (codigoEstatus == HttpStatusCode.Unauthorized)
            {
                return "No está autorizado para realizar esta acción";
            }
            else if (codigoEstatus == HttpStatusCode.Forbidden)
            {
                return "No tiene permisos para realizar esta acción";
            }
            else if (codigoEstatus == HttpStatusCode.InternalServerError)
            {
                return "Error en el servidor, favor de intentar más tarde";
            }
            else
            {
                return "Error inesperado";
            }
        }

        
    }
}
