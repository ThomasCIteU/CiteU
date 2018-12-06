using DatabaseAccess.Langue;
using System.Collections.Generic;

namespace CiteU.Models.Langue
{
    public class LanguesViewModel
    {
        public List<LangueViewModel> ListLangues { get; set; }
    }

    public class LangueViewModel
    {
        public int IdLangue { get; set; }
        public string Nom { get; set; }

    }
}
