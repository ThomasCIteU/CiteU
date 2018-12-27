using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatabaseAccess.BDD;
using DatabaseAccess.User;

namespace DatabaseAccess.Participation
{
    public class ParticipationRepository : IParticipationRepository
    {
        public void CreateParticipation(int idReunion, int idUser)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"INSERT INTO participation (IdReunion, IdUser) VALUES( " +
                    $"@IdReunion, " +
                    $"@IdUser)";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@IdReunion", idReunion);
                cmd.Parameters.AddWithValue("@IdUser", idUser);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteParticipation(int IdParticipation)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"Delete from participation " +
                    $"WHERE idParticipation=@IdParticipation";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@IdParticipation", IdParticipation);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserModel> GetUsersByReunion(int idReunion)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT U.idUser, U.Nom, U.Prenom, U.Sexe, U.Mail, U.Phone, U.Assemblee, U.Privilege, U.idDroit " +
                    "FROM user U " +
                    "LEFT JOIN citeu.participation p on p.idUser= U.idUser " +
                    "WHERE p.idReunion = @idReunion";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idReunion", idReunion);
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
                            Droit = Convert.ToInt16(rdr["idDroit"])
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
    }
}
