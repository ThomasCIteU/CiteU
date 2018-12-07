using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiteU.Models.Login
{
    public class LoginViewModel
    {
        public LoginViewModel(string message)
        {
            ErrorMessage = message;
        }
        public string Mail { get; set; }
        public string Mdp { get; set; }
        public string ErrorMessage { get; set; }
    }
}
