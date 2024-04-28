using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Backend.Data;
using petConnection.Backend.Helpers;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Repositories.Implementations
{
    public class SuccessCasesRepository : GenericRepository<SuccessCase>, ISuccessCasesRepository
    {
        private readonly DataContext _context;

        public SuccessCasesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<SuccessCase>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.SuccessCases.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<SuccessCase>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.SuccessCases.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }        
    }
}