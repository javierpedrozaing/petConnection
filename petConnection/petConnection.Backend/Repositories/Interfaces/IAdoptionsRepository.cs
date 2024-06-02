using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Repositories.Interfaces
{
	public interface IAdoptionsRepository
	{
		Task<ActionResponse<AdoptionDTO>> AddAdoptionAsync(string email, AdoptionDTO adoptionDTO);

        Task<ActionResponse<IEnumerable<Adoption>>> GetAdoptionAsync(string email);

        Task<ActionResponse<int>> GetCountAsync(string email);
    }
}

