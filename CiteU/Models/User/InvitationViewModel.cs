using DatabaseAccess.Droit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.User
{
    public class InvitationViewModel
    {
        public List<DroitModel> AllDroits { get; set; }
        [Required]
        public string MailUser { get; set; }
        public int DroitUser { get; set; }
    }


}
