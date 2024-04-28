using System;
using Microsoft.EntityFrameworkCore;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Data
{
	public class SeedDbUsers
	{
        private readonly DataContext _context;

        public SeedDbUsers(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckUsersAsync();
        }

        private async Task CheckUsersAsync()
        {
            if (_context.Users.Count() <= 1)
            {
                // Seed Users
                var users = new List<User>();
                var random = new Random();
                for (int i = 1; i <= 12; i++)
                {
                    var user = new User
                    {                        
                        UserName = $"userexample{i + 1}",
                        Email = $"userexample{i + 1}@example.com",
                        Password = "password",
                        Role = "User",
                        Profile = new Profile
                        {
                            Name = $"User{i + 1}",
                            LastName = "Doe",
                            Age = random.Next(18, 70), // Random age between 18 and 70
                            Address = $"{random.Next(100, 10000)} Main St",
                            Role = "User",
                            Phone = $"123-456-789{i + 1}",
                            Photo = "path/to/photo.jpg",
                        },                        
                    };
                    users.Add(user);
                }

                // Add to context
                _context.AddRange(users);
                // Save changes
                await _context.SaveChangesAsync();
            }
        }

    }
}

