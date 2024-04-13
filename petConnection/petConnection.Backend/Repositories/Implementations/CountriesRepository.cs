using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Backend.Data;
using petConnection.Backend.Helpers;
using petConnection.Backend.Repositories.Implementations;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.UnitOfWork.Implementations
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly DataContext _context;
        public CountriesRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Countries
                .Include(c => c.States)
                .AsQueryable();

            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async Task<ActionResponse<Country>> GetAsync(int id)
        {
            var country = await _context.Countries
               .Include(c => c.States)
               .ThenInclude(s => s.Cities)
               .FirstOrDefaultAsync(c => c.Id == id);

            if (country == null)
            {
                return new ActionResponse<Country>
                {
                    WasSuccess = false,
                    Message = "País no existe"
                };
            }

            return new ActionResponse<Country>
            {
                WasSuccess = true,
                Result = country
            };
        }

        public override async Task<ActionResponse<IEnumerable<Country>>> GetAsync()
        {
            var countries = await _context.Countries
                .OrderBy(x => x.Name)
                .ToListAsync();
            return new ActionResponse<IEnumerable<Country>>
            {
                WasSuccess = true,
                Result = countries
            };
        }
    }
}

