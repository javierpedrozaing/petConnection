using System;
namespace petConnection.FrontEnd.Shared.Responses
{
    public class ActionResponse<T> // se crea con este nombre para evitar conflictos con clases de azure
    {
        public bool WasSuccess { get; set; }
        public string? Message { get; set; } // signo de ? significa que es una propiedad opcional

        public T? Result { get; set; }
    }
}

