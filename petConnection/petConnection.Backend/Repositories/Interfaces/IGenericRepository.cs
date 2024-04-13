using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;

namespace petConnection.Backend.Repositories.Interfaces
{
    public interface IGenericRespository<T> where T : class // validation where T is class
    {
        Task<ActionResponse<T>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<T>>> GetAsync(); // devovler lista de una entidad

        Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<T>> AddAsync(T entity);

        Task<ActionResponse<T>> DeleteAsync(int id);

        Task<ActionResponse<T>> UpdateAsync(T entity);
    }
}
