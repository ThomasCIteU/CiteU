using DatabaseAccess.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Login
{
    public interface ILoginRepository
    {
        UserModel Login(string Mail, string Mdp);
    }
}
