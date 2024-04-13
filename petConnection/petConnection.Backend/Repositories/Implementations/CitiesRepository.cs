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
    public class CitiesRepository : GenericRepository<City>, ICitiesRepository
    {
        private readonly DataContext _context;

        public CitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(x => x.State!.Id == pagination.Id)
                .AsQueryable();

            return new ActionResponse<IEnumerable<City>>
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
            var queryable = _context.Cities
                .Where(x => x.State!.Id == pagination.Id)
                .AsQueryable();

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