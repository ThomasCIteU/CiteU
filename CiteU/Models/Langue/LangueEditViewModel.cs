using DatabaseAccess.Langue;
using DatabaseAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.Langue
{
    public class LangueEditViewModel : EditorPageViewModel
    {
        public LangueModel CurrentLangue {get; set;}
    }
}
