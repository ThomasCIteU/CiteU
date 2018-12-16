using DatabaseAccess.BDD;
using MySql.Data.MySqlClient;
using System;
using DatabaseAccess.User;

namespace DatabaseAccess.Login
{
    public class LoginRepository : ILoginRepository
    {
        public UserModel Login(string Mail, string Mdp)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM user where Mail = @Mail";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Mail", Mail);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if(rdr.HasRows)
                {
                    if (rdr.Read())
                    {
                        if(Mdp == rdr["Mdp"].ToString())
                        {
                            var user = new UserModel()
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
                            cnn.Close();
                            rdr.Close();
                            return user;
                        }
                    }
                    
                }
                rdr.Close();
                cnn.Close();
                return new UserModel()
                {
                    IdUser = 0
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
