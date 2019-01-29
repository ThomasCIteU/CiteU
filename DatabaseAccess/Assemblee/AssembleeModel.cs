using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseAccess.Assemblee
{
    public class AssembleeModel
    {
        public int IdAssemblee { get; set; }
        [Required]
        public string Nom { get; set; }
        public int IdPole{ get; set; }
    }
}
