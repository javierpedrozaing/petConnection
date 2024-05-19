using System;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
    public class CountriesUnitOfWork : GenericUnitOfWork<Country>, ICountriesUnitOfWork
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesUnitOfWork(IGenericRespository<Country> repository, ICountriesRepository countriesRepository) : base(repository)
        {
            _countriesRepository = countriesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync() => await _countriesRepository.GetAsync();

        public override async Task<ActionResponse<Country>> GetAsync(int id) => await _countriesRepository.GetAsync(id);        

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) => await _countriesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _countriesRepository.GetTotalPagesAsync(pagination);

        public async Task<IEnumerable<Country>> GetComboAsync() => await _countriesRepository.GetComboAsync();
    }
}

