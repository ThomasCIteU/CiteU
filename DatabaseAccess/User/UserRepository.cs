using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatabaseAccess.BDD;



namespace DatabaseAccess.User
{
    public class UserRepository : IUserRepository
    {
        public List<UserModel> GetUsers()
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT idUser, Nom, Prenom, Sexe, Mail, Phone, Assemblee, Privilege FROM User U ";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                var listUsers = new List<UserModel>();
                while (rdr.Read())
                {
                    listUsers.Add(
                        new UserModel()
                        {   
                        IdUser = Convert.ToInt16(rdr["idUser"]),
                        Nom = rdr["Nom"].ToString(),
                        Prenom = rdr["Prenom"].ToString(),
                        Sexe = Convert.ToChar(rdr["Sexe"]),
                        Mail = rdr["Mail"].ToString(),
                        Phone = rdr["Phone"].ToString(),
                        IdAssemblee = Convert.ToInt16(rdr["Assemblee"]),
                        Privilege = rdr["Privilege"].ToString()
                        }
                    );
                }
                rdr.Close();

                return listUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetUser(int idUser)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM User WHERE iduser=@idUser";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idUser", idUser);
                MySqlDataReader rdr = cmd.ExecuteReader();
                UserModel user = null;
                if (rdr.Read())
                {
                    user = new UserModel()
                    {
                        IdUser = Convert.ToInt16(rdr["idUser"]),
                        Nom = rdr["Nom"].ToString(),
                        Prenom = rdr["Prenom"].ToString(),
                        Sexe = Convert.ToChar(rdr["Sexe"]),
                        Mail = rdr["Mail"].ToString(),
                        Phone = rdr["Phone"].ToString(),
                        IdAssemblee = Convert.ToInt16(rdr["Assemblee"]),
                        Privilege = rdr["Privilege"].ToString()
                    };
                }
                rdr.Close();

                if(user == null)
                {
                    throw new Exception("Cet utilisateur n'existe pas");
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditUser(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"UPDATE user SET " +
                    $"Nom = @Nom, " +
                    $"Prenom = @Prenom, " +
                    $"Sexe = @Sexe, " +
                    $"Mail = @Mail, " +
                    $"Phone = @Phone, " +
                    $"Assemblee = @Assemblee, " +
                    $"Privilege = @Privilege " +
                    $"WHERE idUser=@idUser";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Nom", Nom);
                cmd.Parameters.AddWithValue("@Prenom", Prenom);
                cmd.Parameters.AddWithValue("@Sexe", Sexe.ToString());
                cmd.Parameters.AddWithValue("@Mail", Mail);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Assemblee", Assemblee);
                cmd.Parameters.AddWithValue("@Privilege", Privilege);
                cmd.Parameters.AddWithValue("@idUser", IdUser);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUser(int IdUser)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"Delete from user " +
                    $"WHERE idUser=@idUser";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idUser", IdUser);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateUser(string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"INSERT INTO user (Nom, Prenom, Sexe, Mail, Phone, Assemblee, Privilege) VALUES( " +
                    $"@Nom, " +
                    $"@Prenom, " +
                    $"@Sexe, " +
                    $"@Mail, " +
                    $"@Phone, " +
                    $"@Assemblee, " +
                    $"@Privilege)";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Nom", Nom);
                cmd.Parameters.AddWithValue("@Prenom", Prenom);
                cmd.Parameters.AddWithValue("@Sexe", Sexe);
                cmd.Parameters.AddWithValue("@Mail", Mail);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Assemblee", Assemblee);
                cmd.Parameters.AddWithValue("@Privilege", Privilege);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
