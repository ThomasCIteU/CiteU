using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatabaseAccess.BDD;



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
    }
}
