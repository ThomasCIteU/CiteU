using DatabaseAccess.Pole;
using DatabaseAccess.Reunion;
using DatabaseAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.Reunion
{
    public class ReunionEditViewModel : EditorPageViewModel
    {
        public ReunionModel CurrentReunion {get; set;}
        public List<UserModel> AllUsers {get; set;}
        public List<PoleModel> AllPoles {get; set;}
    }
}
