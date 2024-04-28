using System;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
    public class SuccessCasesUnitOfWork : GenericUnitOfWork<SuccessCase>, ISuccessCasesUnitOfWork
    {
        private readonly ISuccessCasesRepository _successCasesRepository;

        public SuccessCasesUnitOfWork(IGenericRespository<SuccessCase> repository, ISuccessCasesRepository successCasesRepository) : base(repository)
        {
                _successCasesRepository = successCasesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<SuccessCase>>> GetAsync(PaginationDTO pagination) => await _successCasesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _successCasesRepository.GetTotalPagesAsync(pagination);
    }    
}