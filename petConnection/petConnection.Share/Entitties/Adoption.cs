using System;
using System.ComponentModel.DataAnnotations;

namespace petConnection.Share.Entitties
{
	public class Adoption
	{
		public int Id { get; set; }

		public User? User { get; set; }
		public string? UserId { get; set; }

		public Pet? Pet { get; set; }
		public int PetId { get; set; }

		[Display(Name = "Fecha de adopción")]
		public DateTime AdoptionDate { get; set; }

		[Display(Name = "Estado de adopción")]
		public string? Status { get; set; }

	}
}

