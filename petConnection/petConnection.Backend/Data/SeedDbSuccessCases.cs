using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Data
{
    public class SeedDbSuccessCases
    {
        private readonly DataContext _context;

        public SeedDbSuccessCases(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckSuccessCasesAsync();
        }

        private async Task CheckSuccessCasesAsync()
        {
            if (_context.SuccessCases.Count() <= 1)
            {
                var successCases = new List<SuccessCase>();
                var random = new Random();
                for (int i = 1; i <= 12; i++)
                {
                    var successCase = new SuccessCase
                    {
                        Name = $"success case{i + 1}",
                        Description = $"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolorcase",
                        User_id = 1,
                        Date = DateTime.Now,
                        Status = "Published",
                        Pet = new Pet
                        {
                            Name = "Pet A",
                            Specie = "asdasd",
                            Race = " dfsdfsdf",
                            Age = random.Next(1, 15),
                            Gender = "Macho",
                            Size = "",
                            Weight = $"{random.Next(5, 50)} lbs",
                            Color = "Negro",
                            HealthCondition = "Good",
                            Behavior = "Behavior",
                            Photo = "https://upload.wikimedia.org/wikipedia/commons/6/6b/Icecat1-300x300.svg"
                        },

                    };
                    successCases.Add(successCase);
                }
                _context.AddRange(successCases);
            }            
            await _context.SaveChangesAsync();
        }        
    }
}