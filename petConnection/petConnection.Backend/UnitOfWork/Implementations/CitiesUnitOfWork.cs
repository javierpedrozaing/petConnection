using System;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
    public class CitiesUnitOfWork : GenericUnitOfWork<City>, ICitiesUnitOfWork
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesUnitOfWork(IGenericRespository<City> repository, ICitiesRepository citiesRepository) : base(repository)
        {
            _citiesRepository = citiesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination) => await _citiesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _citiesRepository.GetTotalPagesAsync(pagination);
    }
}

