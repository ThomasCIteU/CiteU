using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.User
{
    public interface IUserRepository
    {
        List<UserModel> GetUsers();
        UserModel GetUser(int idUser);
        void EditUser(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, string Assemblee, string Privilege);
        void DeleteUser(int IdUser);
    }
}
