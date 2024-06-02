using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Interfaces
{
	public interface IAdoptionsUnitOfWork
	{
        Task<ActionResponse<AdoptionDTO>> AddAdoptionAsync(string email, AdoptionDTO adoptionDTO);

        Task<ActionResponse<IEnumerable<Adoption>>> GetAdoptionAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);
    }
}

