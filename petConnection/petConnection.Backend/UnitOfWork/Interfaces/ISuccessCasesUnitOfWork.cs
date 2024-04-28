using System;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Interfaces
{
    public interface ISuccessCasesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<SuccessCase>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);
    }
}