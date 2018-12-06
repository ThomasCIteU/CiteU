using DatabaseAccess.Pole;
using DatabaseAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.Pole
{
    public class PoleEditViewModel : EditorPageViewModel
    {
        public PoleModel CurrentPole {get; set;}
        public List<UserModel> AllUsers {get; set;}
    }
}
