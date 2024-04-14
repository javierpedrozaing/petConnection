using System;
namespace petConnection.Share.Entitties
{
	public class Article
	{
		public int Id { get; set; }

		public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string PhotoPath { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

