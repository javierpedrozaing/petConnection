using System;
namespace petConnection.FrontEnd.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(String url);

        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model); // post que no devuelve respuesta

        Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model); // post que devuelve respuesta

        Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url);

        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);

        Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model);

        Task<HttpResponseWrapper<object>> GetAsync(string url);
    }
}