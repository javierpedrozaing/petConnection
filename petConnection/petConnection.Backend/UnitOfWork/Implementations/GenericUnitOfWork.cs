using System;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;

namespace petConnection.Backend.UnitOfWork.Implementations
{
    public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
    {
        private readonly IGenericRespository<T> _repository;

        public GenericUnitOfWork(IGenericRespository<T> repository)
        {
            _repository = repository; // el underscore tambien nos permite no tener que utilizar la palabra this
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination) => await _repository.GetAsync(pagination);

        public virtual async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _repository.GetTotalPagesAsync(pagination);

        public virtual async Task<ActionResponse<T>> AddAsync(T model) => await _repository.AddAsync(model);

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public virtual async Task<ActionResponse<T>> GetAsync(int id) => await _repository.GetAsync(id);

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

        public virtual async Task<ActionResponse<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
    }
}

