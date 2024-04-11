using System;
using System.ComponentModel.DataAnnotations;

namespace petConnection.Share.Entitties
{
	public class Pet
	{
        public int Id { get; set; }

        public int UserId { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Specie { get; set; }

        [Required]
        public string Race { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Size { get; set; }
         
        [Required]
        public string Weight { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string HealthCondition { get; set; }

        [Required]
        public string Behavior { get; set; }

        // temporal commented
        //[Required]
        //public string Photo { get; set; }

        public User? User { get; set; }
    }
}

