using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseAccess.User
{
    public class UserModel
    {
        public int IdUser { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public char Sexe { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string Phone { get; set; }
        public int IdAssemblee { get; set; }
        public string Privilege { get; set; }
        public int Droit { get; set; }
        [Required]
        public string Mdp { get; set; }
    }
}
