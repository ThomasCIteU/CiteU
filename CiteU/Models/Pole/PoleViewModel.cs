using DatabaseAccess.Pole;
using System.Collections.Generic;

namespace CiteU.Models.Pole
{
    public class PolesViewModel
    {
        public List<PoleViewModel> ListPoles { get; set; }
    }

    public class PoleViewModel
    {
        public int IdPole { get; set; }
        public string Libelle { get; set; }
        public int IdResponsable { get; set; }
        public string NomResponsable{ get; set; }
        public int IdAdjoint { get; set; }
        public string NomAdjoint { get; set; }

    }
}
