using System;
using System.ComponentModel.DataAnnotations;

namespace petConnection.Share.Entitties
{
	public class User
	{
		public int Id { get; set; }

		[MaxLength(255)]
		public string Role { get; set; } = null!;

		[MaxLength(255)]
		public string UserName {get; set; } = null!;

        [Required]
		public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public Profile Profile { get; set; } = null!;

        public ICollection<Pet>? Pets { get; set; }

        [Display(Name = "Mascotas")]
        public int PetsNumber => Pets == null || Pets.Count == 0 ? 0 : Pets.Count;
    }	
}

