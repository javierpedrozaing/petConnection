using System;
using System.Net;

namespace petConnection.FrontEnd.Repositories
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpresponsemessage)
        {
            Response = response;
            Error = error;
            Httpresponsemessage = httpresponsemessage;
        }

        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage Httpresponsemessage { get; }

        public async Task<string?> GetErrorMessageAsync() // envoltorio para mejorar las respuestas de peticiones Http
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = Httpresponsemessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            if (statusCode == HttpStatusCode.BadRequest)
            {
                return await Httpresponsemessage.Content.ReadAsStringAsync();
            }
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar logueado para ejecutar esta operación";
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permiso para ejecutar esta operación";
            }

            return "Ha ocurrido un error";
        }
    }
}

