using DatabaseAccess.Assemblee;
using DatabaseAccess.Pole;
using DatabaseAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.Assemblee
{
    public class AssembleeEditViewModel : EditorPageViewModel
    {
        public AssembleeModel CurrentAssemblee {get; set;}
        public List<PoleModel> AllPoles {get; set;}
    }
}
