using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Interfaces
{
	public interface ICountriesUnitOfWork
	{
        Task<ActionResponse<IEnumerable<Country>>> GetAsync();

        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination);
    }
}

