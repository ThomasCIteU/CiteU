using DatabaseAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.User
{
    public class UsersViewModel
    {
        public List<UserViewModel> ListUsers { get; set; }
    }
    public class UserViewModel
    {
        public int IdUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public char Sexe { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public int IdAssemblee { get; set; }
        public string NomAssemblee { get; set; }
        public string Privilege { get; set; }
    }
}
