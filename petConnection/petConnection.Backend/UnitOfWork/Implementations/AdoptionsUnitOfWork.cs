using System;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.Backend.UnitOfWork.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
    public class AdoptionsUnitOfWork : GenericUnitOfWork<Adoption>, IAdoptionsUnitOfWork
    {        
        private readonly IAdoptionsRepository _adoptionsRepository;

        public AdoptionsUnitOfWork(IGenericRespository<Adoption> repository, IAdoptionsRepository adoptionsRepository) : base(repository)
        {            
            _adoptionsRepository = adoptionsRepository;
        }

        public async Task<ActionResponse<AdoptionDTO>> AddAdoptionAsync(string email, AdoptionDTO adoptionDTO) => await _adoptionsRepository.AddAdoptionAsync(email, adoptionDTO);

        public async Task<ActionResponse<IEnumerable<Adoption>>> GetAdoptionAsync(string email) => await _adoptionsRepository.GetAdoptionAsync(email);

        public async Task<ActionResponse<int>> GetCountAsync(string email) => await _adoptionsRepository.GetCountAsync(email);
    }
}

