using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckPetsAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Medellín" },
                                new City() { Name = "Intagui" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Bello" },
                                new City() { Name = "Rionegro" }
                            }
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Usaquen" },
                                new City() { Name = "Chapinero" },
                                new City() { Name = "Santa fe" },
                                new City() { Name = "Usme" },
                                new City() { Name = "Bosa" }
                            }
                        }
                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Florida",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key West" }
                            }
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = new List<City>()
                            {
                                new City() { Name = "Houston" },
                                new City() { Name = "San Antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El paso" }
                            }
                        }
                    }
                });
            }

            await _context.SaveChangesAsync();
        }


        private async Task CheckPetsAsync()
        {
            _context.Database.EnsureCreated();

            if (!_context.Users.Any())
            {
                // Seed Users
                var users = new List<User>
            {
                new User
                {
                    UserName = "user1",
                    Email = "user1@example.com",
                    Password = "password",
                    Role = "Admin",
                    Profile = new Profile
                    {
                        Name = "John",
                        LastName = "Doe",
                        Age = 30,
                        Address = "123 Main St",
                        Role = "Admin",
                        Phone = "123-456-7890",
                        Photo = "path/to/photo.jpg"
                    },
                    Pets = new List<Pet>()
                }
            };

                // Seed Pets
                var random = new Random();
                foreach (var user in users)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var pet = new Pet
                        {
                            Name = GenerateRandomNames(i).First(),
                            Specie = GetRandomSpecie(),
                            Race = GetRandomRace(),
                            Age = random.Next(1, 15),
                            Gender = GetRandomGender(),
                            Size = GetRandomSize(),
                            Weight = $"{random.Next(5, 50)} lbs",
                            Color = GetRandomColor(),
                            HealthCondition = GetRandomHealthCondition(),
                            Behavior = GetRandomBehavior(),
                            Photo = "path/to/photo.jpg"
                        };
                        user.Pets.Add(pet);
                    }
                }
                // Add to context and save changes
                _context.AddRange(users);
                _context.SaveChanges();
            }         
        }

        private string[] GenerateRandomNames(int count)
        {
            string[] names = new string[count];
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                names[i] = GenerateRandomName(rand);
            }

            return names;
        }

        private string GenerateRandomName(Random rand)
        {
            string[] petNames = {
                "Buddy", "Max", "Charlie", "Jack", "Cooper", "Rocky", "Bear", "Duke",
                "Toby", "Tucker", "Jake", "Zeus", "Oscar", "Bailey", "Milo", "Rusty",
                "Simba", "Murphy", "Winston", "Sam"
            };

            int index = rand.Next(petNames.Length);

            return petNames[index];
        }

        private string GetRandomSpecie()
        {
            string[] species = { "Dog", "Cat", "Bird", "Fish", "Hamster", "Rabbit" };
            Random rand = new Random();
            return species[rand.Next(species.Length)];
        }

        private string GetRandomRace()
        {
            string[] races = { "Labrador", "Siamese", "Parrot", "Goldfish", "Syrian", "Holland Lop" };
            Random rand = new Random();
            return races[rand.Next(races.Length)];
        }

        private string GetRandomGender()
        {
            string[] genders = { "Male", "Female" };
            Random rand = new Random();
            return genders[rand.Next(genders.Length)];
        }

        private string GetRandomSize()
        {
            string[] sizes = { "Small", "Medium", "Large" };
            Random rand = new Random();
            return sizes[rand.Next(sizes.Length)];
        }

        private string GetRandomColor()
        {
            string[] colors = { "Black", "White", "Brown", "Gray", "Orange", "Mixed" };
            Random rand = new Random();
            return colors[rand.Next(colors.Length)];
        }

        private string GetRandomHealthCondition()
        {
            string[] healthConditions = { "Healthy", "Sick", "Injured" };
            Random rand = new Random();
            return healthConditions[rand.Next(healthConditions.Length)];
        }

        private string GetRandomBehavior()
        {
            string[] behaviors = { "Friendly", "Aggressive", "Calm", "Playful" };
            Random rand = new Random();
            return behaviors[rand.Next(behaviors.Length)];
        }
    }
}

