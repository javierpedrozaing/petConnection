using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
	public interface ICountriesRepository
	{
        Task<ActionResponse<Country>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Country>>> GetAsync(); // devovler lista de una entidad
    }
}

