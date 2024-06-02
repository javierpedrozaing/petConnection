using System;
namespace petConnection.Share.DTOs
{
	public class AdoptionDTO
	{
		public int UserId { get; set; }
		public int PetId { get; set; }
		public string Status { get; set; } = "";
	}
}

