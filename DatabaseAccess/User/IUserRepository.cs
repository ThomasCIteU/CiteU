using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.User
{
    public interface IUserRepository
    {
        List<UserModel> GetUsers();
        UserModel GetUser(int idUser);
        void EditUser(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege, int Droit);
        void DeleteUser(int IdUser);
        void CreateUser(string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege, int Droit);
    }
}
