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
                string sql = "SELECT idUser, Nom, Prenom, Sexe, Mail, Phone, Assemblee, Privilege, idDroit, Mdp FROM user U Where idUser <> 0 ";
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
                        Privilege = rdr["Privilege"].ToString(),
                        Droit = Convert.ToInt16(rdr["idDroit"]),
                        Mdp = rdr["Mdp"].ToString()
                        }
                    );
                }
                rdr.Close();
                cnn.Close();
                return listUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserModel> GetUsersByPole(int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT U.idUser, U.Nom, U.Prenom, U.Sexe, U.Mail, U.Phone, U.Assemblee, U.Privilege, U.idDroit, U.Mdp " +
                    "FROM user U " +
                    "LEFT JOIN assemblee a on a.idassemblee = U.Assemblee " +
                    "WHERE a.IdPole = @idPole";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idPole", idPole);
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
                            Privilege = rdr["Privilege"].ToString(),
                            Droit = Convert.ToInt16(rdr["idDroit"]),
                            Mdp = rdr["Mdp"].ToString()
                        }
                    );
                }
                rdr.Close();
                cnn.Close();
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
                string sql = "SELECT * FROM user WHERE iduser=@idUser";
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
                        Privilege = rdr["Privilege"].ToString(),
                        Droit = Convert.ToInt16(rdr["idDroit"]),
                        Mdp = rdr["Mdp"].ToString()
                    };
                }
                rdr.Close();
                cnn.Close();
                if (user == null)
                {
                    throw new Exception("Cet utilisateur n'existe pas");
                }
                user.LanguesParlees = GetLanguesParlees(idUser);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserModel GetUser(string mailUser)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM user WHERE Mail=@mailUser";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@mailUser", mailUser);
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
                        Privilege = rdr["Privilege"].ToString(),
                        Droit = Convert.ToInt16(rdr["idDroit"]),
                        Mdp = rdr["Mdp"].ToString()
                    };
                }
                rdr.Close();
                cnn.Close();
                if (user == null)
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

        public void EditUser(int IdUser, string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege, int Droit, string Mdp, List<int> languesParlees)
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
                    $"Privilege = @Privilege, " +
                    $"idDroit = @Droit, " +
                    $"Mdp = @Mdp " +
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
                cmd.Parameters.AddWithValue("@Mdp", Mdp);
                cmd.Parameters.AddWithValue("@Droit", Droit);

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            AddAllLanguages(languesParlees, IdUser);
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

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateUser(string Nom, string Prenom, char Sexe, string Mail, string Phone, int Assemblee, string Privilege, int Droit, string Mdp, List<int> languesParlees)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"INSERT INTO user (Nom, Prenom, Sexe, Mail, Phone, Assemblee, Privilege, idDroit, Mdp) VALUES( " +
                    $"@Nom, " +
                    $"@Prenom, " +
                    $"@Sexe, " +
                    $"@Mail, " +
                    $"@Phone, " +
                    $"@Assemblee, " +
                    $"@Privilege, " +
                    $"@Droit," +
                    $"@Mdp)";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Nom", Nom);
                cmd.Parameters.AddWithValue("@Prenom", Prenom);
                cmd.Parameters.AddWithValue("@Sexe", Sexe);
                cmd.Parameters.AddWithValue("@Mail", Mail);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Assemblee", Assemblee);
                cmd.Parameters.AddWithValue("@Privilege", Privilege);
                cmd.Parameters.AddWithValue("@Droit", Droit);
                cmd.Parameters.AddWithValue("@Mdp", Mdp);

                cmd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if(languesParlees != null)
            {
                //Ajouter toutes les langues
                var usr = GetUser(Mail);
                AddAllLanguages(languesParlees, usr.IdUser);
            }
        }

        public void AddAllLanguages (List<int> langues, int idUser)
        {
            if(langues == null)
            {
                return;
            }
            string sqlDelete = $"DELETE FROM languesparlees WHERE ";
            //on ajoute les langues
            foreach (int idlangue in langues)
            {
                sqlDelete += $"idLangues <> "+idlangue.ToString()+" AND ";
                MySqlConnection cnn = BDDRepository.OpenConnexion();
                try
                {
                    string sql = $"INSERT IGNORE INTO languesparlees (idUser, idLangues) VALUES( " +
                    $"@IdUser, " +
                    $"@IdLangues)";

                    MySqlCommand cmd = new MySqlCommand(sql, cnn);
                    cmd.Parameters.AddWithValue("@IdUser", idUser);
                    cmd.Parameters.AddWithValue("@IdLangues", idlangue);

                    cmd.ExecuteNonQuery();

                    cnn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            //on supprime les langues supprimées
            MySqlConnection cnn2 = BDDRepository.OpenConnexion();
            try
            {
                sqlDelete += $"idUser = @idUser";

                MySqlCommand cmd = new MySqlCommand(sqlDelete, cnn2);
                cmd.Parameters.AddWithValue("@IdUser", idUser);

                cmd.ExecuteNonQuery();

                cnn2.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> GetLanguesParlees(int idUser)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT idLangues FROM languesparlees WHERE idUser=@idUser";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idUser", idUser);
                MySqlDataReader rdr = cmd.ExecuteReader();
                List<int> languesparlees = new List<int>();
                while (rdr.Read())
                {
                    languesparlees.Add(Convert.ToInt16(rdr["idLangues"]));
                }
                rdr.Close();
                cnn.Close();
                return languesparlees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
