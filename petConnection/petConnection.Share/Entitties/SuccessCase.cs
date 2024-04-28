using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace petConnection.Share.Entitties
{
    public class SuccessCase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Pet Pet { get; set; } = null!;
        public int User_id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;

        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Estado")]
        public string Status { get; set; } = null!;
    }
}

