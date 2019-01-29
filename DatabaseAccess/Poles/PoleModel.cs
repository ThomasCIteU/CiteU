using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseAccess.Pole
{
    public class PoleModel
    {
        public int IdPole { get; set; }
        [Required]
        public string Libelle { get; set; }
        [Required]
        public int IdResponsable { get; set; }
        [Required]
        public int IdAdjoint { get; set; }
    }
}
