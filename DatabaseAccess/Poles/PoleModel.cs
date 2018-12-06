using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Pole
{
    public class PoleModel
    {
        public int IdPole { get; set; }
        public string Libelle { get; set; }
        public int IdResponsable{ get; set; }
        public int IdAdjoint { get; set; }
    }
}
