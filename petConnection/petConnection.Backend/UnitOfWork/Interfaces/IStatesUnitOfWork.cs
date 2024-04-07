using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Interfaces
{
    public interface IStatesUnitOfWork
    {

        Task<ActionResponse<IEnumerable<State>>> GetAsync();

        Task<ActionResponse<State>> GetAsync(int id);
    }
}

