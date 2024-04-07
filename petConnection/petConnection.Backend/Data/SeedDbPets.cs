using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Data
{
	public class SeedDbPets
	{
        private readonly DataContext _context;
        public SeedDbPets(DataContext context)
		{
			_context = context;
		}

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckPetsAsync();
        }

        private async Task CheckPetsAsync()
        {
            if (!_context.Users.Any())
            {
                var javierProfile = new Profile
                {
                    Name = "Javier",
                    LastName = "Pedroza",
                    Age = 35,
                    Address = "calle 100",
                    Role = "Admin",
                    Phone = "31111111111",
                };

                var javier = new User
                {
                    UserName = "javier",
                    Email = "javierpedrozaing@gmail.com",
                    Password = "12345678",
                    Profile = javierProfile,
                };

                var heilerProfile = new Profile
                {
                    Name = "Heiler",
                    LastName = "Ruiz",
                    Age = 30,
                    Address = "calle 100",
                    Role = "Admin",
                    Phone = "31111111111",
                };

                var heiler = new User
                {
                    UserName = "heler",
                    Email = "heiler@gmail.com",
                    Password = "12345678",
                    Profile = heilerProfile,
                };


                _context.Users.AddRange(javier, heiler);

                var random = new Random();
                var pets = new List<Pet>();

                for (int i = 0; i < 12; i++)
                {
                    var specie = random.Next(2) == 0 ? "Dog" : "Cat";
                    var pet = new Pet
                    {
                        UserId = i < 6 ? javier.Id : heiler.Id, // Assigning pets to users alternatively
                        Name = $"Pet{i + 1}",
                        Specie = specie,
                        Race = specie == "Dog" ? "Labrador" : "Persian",
                        Age = random.Next(1, 15),
                        Gender = random.Next(2) == 0 ? "Male" : "Female",
                        Size = random.Next(2) == 0 ? "Small" : "Large",
                        Weight = random.Next(1, 50).ToString(),
                        Color = $"Color{i + 1}",
                        HealthCondition = $"Healthy",
                        Behavior = $"Behavior{i + 1}",
                        Photo = $"https://example.com/photo{i + 1}.jpg" // Assuming photo URLs
                    };
                    pets.Add(pet);
                }
                _context.Pets.AddRange(pets);
                await _context.SaveChangesAsync();
            }
        }
        
    }
}

