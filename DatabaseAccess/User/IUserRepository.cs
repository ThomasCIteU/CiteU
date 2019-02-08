using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.User
{
    public interface IUserRepository
    {
        List<UserModel> GetUsers();
        List<UserModel> GetUsersByPole(int idPole);
        UserModel GetUser(int idUser);
        UserModel GetUser(string mailUser);
        void EditUser(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege, int Droit, string Mdp, List<int> languesParlees);
        void DeleteUser(int IdUser);
        void CreateUser(string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege, int Droit, string Mdp, List<int> languesParlees);
    }
}
