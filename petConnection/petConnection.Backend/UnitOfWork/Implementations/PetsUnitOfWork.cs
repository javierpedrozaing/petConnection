using System;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
	public class PetsUnitOfWork : GenericUnitOfWork<Pet>, IPetsUnitOfWork
    {
        private readonly IPetsRepository _petsRepository;

    public PetsUnitOfWork(IGenericRespository<Pet> repository, IPetsRepository petsRepository) : base(repository)
    {
            _petsRepository = petsRepository;
    }

    public override async Task<ActionResponse<IEnumerable<Pet>>> GetAsync(PaginationDTO pagination) => await _petsRepository.GetAsync(pagination);

    public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _petsRepository.GetTotalPagesAsync(pagination);
    }
}
