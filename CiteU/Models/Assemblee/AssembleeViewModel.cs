using DatabaseAccess.Assemblee;
using System.Collections.Generic;

namespace CiteU.Models.Assemblee
{
    public class AssembleesViewModel
    {
        public List<AssembleeViewModel> ListAssemblees { get; set; }
    }

    public class AssembleeViewModel
    {
        public int IdAssemblee { get; set; }
        public string Nom { get; set; }
        public int IdPole { get; set; }
        public string NomPole { get; set; }
    }
}
