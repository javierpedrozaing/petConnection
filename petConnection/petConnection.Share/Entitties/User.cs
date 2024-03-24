using System;
using System.ComponentModel.DataAnnotations;

namespace petConnection.Share.Entitties
{
	public class User
	{
		public int Id { get; set; }

        [MaxLength(255)]
        public string Role { get; set; }

		[MaxLength(255)]
		public string UserName {get; set;}

		[Required]
		public string Email { get; set; }


		public string Password { get; set; }
	}
	
}

