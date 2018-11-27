using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.User
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public char Sexe { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Assemblee { get; set; }
        public string Privilege { get; set; }
    }
}
