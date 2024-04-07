using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using petConnection.Share.Interfaces;

namespace petConnection.Share.Entitties
{
	public class City : IEntityWithName
	{
        public int id { get; set; }
        public int StateId { get; set; }

        [Display(Name = "Ciudad")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        public State? State { get; set; }
    }
}

