using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync();
    }

}

