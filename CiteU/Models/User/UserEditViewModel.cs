using DatabaseAccess.Assemblee;
using DatabaseAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.User
{
    public class UserEditViewModel : EditorPageViewModel
    {
        public UserModel CurrentUser {get; set;}
        public List<AssembleeModel> AllAssemblees { get; set; }
    }
}
