using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Backend.Data;
using petConnection.Backend.Repositories.Interfaces;
using petConnection.FrontEnd.Shared.Responses;
using petConnection.Share.DTOs;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Repositories.Implementations
{
    public class AdoptionsRepository : GenericRepository<Adoption>, IAdoptionsRepository
    {
        private readonly DataContext _context;
        private readonly IUsersRepository _usersRepository;

        public AdoptionsRepository(DataContext context, IUsersRepository usersRepository) : base(context)
        {
            _context = context;
            _usersRepository = usersRepository;
        }

        public async Task<ActionResponse<AdoptionDTO>> AddAdoptionAsync(string email, AdoptionDTO adoptionDTO)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(x => x.Id == adoptionDTO.PetId);

            if (pet == null)
            {
                return new ActionResponse<AdoptionDTO>
                {
                    WasSuccess = false,
                    Message = "Mascota No encontrada"
                };
            }

            var user = await _usersRepository.GetUserAsync(email);

            if (user == null)
            {
                return new ActionResponse<AdoptionDTO>
                {
                    WasSuccess = false,
                    Message = "Usuario No encontrado"
                };
            }

            var adoption = new Adoption
            {
                Pet = pet,
                User = user,
                Status = "Adopción exitosa",
                AdoptionDate = DateTime.Now
            };

            try
            {
                _context.Add(adoption);
                await _context.SaveChangesAsync();
                return new ActionResponse<AdoptionDTO>
                {
                    WasSuccess = true,
                    Result = adoptionDTO
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<AdoptionDTO>
                {
                    WasSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ActionResponse<IEnumerable<Adoption>>> GetAdoptionAsync(string email)
        {
            var adoptions = await _context.Adoption
                .Include(ad => ad.User!)
                .Include(ad => ad.Pet!)
                .Where(ad => ad.User!.Email == email)
                .ToListAsync();

            return new ActionResponse<IEnumerable<Adoption>>
            {
                WasSuccess = true,
                Result = adoptions
            };
        }

        public async Task<ActionResponse<int>> GetCountAsync(string email)
        {
            var count = await _context.Adoption
                .Where(X => X.User!.Email == email)
                .SumAsync(x => 1);

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = (int)count
            };
        }
    }
}

