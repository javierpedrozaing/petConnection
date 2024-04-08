using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using petConnection.Share.Interfaces;

namespace petConnection.Share.Entitties
{
	public class Profile : IEntityWithName
	{
        public int Id { get; set; }

        [Key]
        public int UserId { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(255)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Apellido")]
        [MaxLength(255)]        
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Age { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Role { get; set; } = null!;

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Phone { get; set; } = null!;

        [Display(Name = "Foto")]
        public string Photo { get; set; } = null!;

        public User? User { get; set; } = null!;
    }
}

