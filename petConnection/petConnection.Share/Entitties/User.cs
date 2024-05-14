using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using petConnection.Share.Enums;

namespace petConnection.Share.Entitties
{
	public class User : IdentityUser
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

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Document { get; set; } = null!;

        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Address { get; set; } = null!;

        [Display(Name = "Foto")]
        public string? Photo { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        public City? City { get; set; }

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        public int CityId { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";
    }	
}

