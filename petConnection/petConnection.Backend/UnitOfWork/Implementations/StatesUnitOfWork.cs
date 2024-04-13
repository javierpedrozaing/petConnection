using System;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
    public class StatesUnitOfWork : GenericUnitOfWork<State>, IStatesUnitOfWork
    {
        public readonly IStatesRepository _statesRepository;

        public StatesUnitOfWork(IGenericRespository<State> repository, IStatesRepository statesRepository) : base(repository)
        {
            _statesRepository = statesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync() => await _statesRepository.GetAsync();

        public override async Task<ActionResponse<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination) => await _statesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _statesRepository.GetTotalPagesAsync(pagination);

    }
}