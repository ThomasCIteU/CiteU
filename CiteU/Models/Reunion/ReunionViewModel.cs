using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.Reunion
{
    public class ReunionsViewModel : EditorPageViewModel
    {
        public List<ReunionViewModel> ListReunions { get; set; }
    }

    public class ReunionViewModel : EditorPageViewModel
    {
        public int IdReunion { get; set; }
        public DateTime Date { get; set; }
        public DateTime Heure { get; set; }
        public int IdResponsable { get; set; }
        public int IdCreateur { get; set; }
        public string Lieu { get; set; }
    }
}
