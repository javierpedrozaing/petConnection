using System;
using System.ComponentModel.DataAnnotations;

namespace petConnection.Share.Entitties
{
	public class Pet
	{
        public int Id { get; set; }

        public int UserId { get; set; }

        [MaxLength(255)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [MaxLength(255)]
        [Display(Name = "Especie")]
        public string Specie { get; set; }

        [Required]
        [Display(Name = "Raza")]
        public string Race { get; set; }

        [Required]
        [Display(Name = "Edad")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gémero")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Tamaño")]
        public string Size { get; set; }
         
        [Required]
        [Display(Name = "Peso")]
        public string Weight { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string HealthCondition { get; set; }

        [Required]
        public string Behavior { get; set; }

        [Required]
        public string Photo { get; set; } = null!;

        public User? User { get; set; }
    }
}

