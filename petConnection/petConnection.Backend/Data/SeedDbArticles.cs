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

                for (int i = 0; i < 20; i++)
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
            return "Random Text " + Guid.NewGuid().ToString().Substring(0, 10);
        }

        private string GenerateRandomPhotoPath()
        {
            return "https://www.shutterstock.com/shutterstock/photos/1983139877/display_1500/stock-vector-creative-writing-and-storytelling-brief-contract-terms-and-conditions-document-paper-assignment-1983139877.jpg";
        }
    }
}

