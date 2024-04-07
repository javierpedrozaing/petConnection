using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using petConnection.Share.Interfaces;

namespace petConnection.Share.Entitties
{
    public class Country : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "País")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;
        public ICollection<State>? States { get; set; }

        [Display(Name = "Departamento / Estados")]
        public int StateNumber => States == null || States.Count == 0 ? 0 : States.Count;
    }
}

