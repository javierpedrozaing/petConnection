using System;
using petConnection.Share.Entitties;

namespace petConnection.Backend.Data
{
	public class SeedDbArticles
	{
		private readonly DataContext _context;

		public SeedDbArticles(DataContext context)
		{
			_context = context;
		}

		public async Task SeedAsync()
		{
			await _context.Database.EnsureCreatedAsync();
			await CheckArticlesAsync();
		}

        private async Task CheckArticlesAsync()
        {
            if (!_context.Articles.Any())
			{
                var articles = new List<Article>();

                for (int i = 0; i < 30; i++)
                {
                    var article = new Article
                    {
                        Title = GenerateRandomTitle(),
                        Text = GenerateRandomText(),
                        PhotoPath = GenerateRandomPhotoPath(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };

                    articles.Add(article);
                };

                _context.AddRange(articles);                
            }
            await _context.SaveChangesAsync();
        }

        private string GenerateRandomTitle()
        {            
            return "Random Title " + Guid.NewGuid().ToString().Substring(0, 5);
        }

        private string GenerateRandomText()
        {            
            string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            
            string[] words = loremIpsum.Split(' ');
            
            Random random = new Random();
            words = words.OrderBy(w => random.Next()).ToArray();
            
            string randomText = string.Join(" ", words.Take(200));

            return randomText;
        }

        private string GenerateRandomPhotoPath()
        {
            return "https://www.shutterstock.com/shutterstock/photos/1983139877/display_1500/stock-vector-creative-writing-and-storytelling-brief-contract-terms-and-conditions-document-paper-assignment-1983139877.jpg";
        }
    }
}

