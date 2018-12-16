using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatabaseAccess.BDD;



namespace DatabaseAccess.Reunion
{
    public class ReunionRepository : IReunionRepository
    {

        public List<ReunionModel> GetAllReunions()
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM reunion";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var reunionModel = new List<ReunionModel>();
                while (rdr.Read())
                {
                    reunionModel.Add(
                        new ReunionModel()
                        {
                            IdReunion = Convert.ToInt16(rdr["idreunion"]),
                            Date = Convert.ToDateTime(rdr["Date"]),
                            IdResponsable= Convert.ToInt16(rdr["IdResponsable"]),
                            IdCreateur = Convert.ToInt16(rdr["IdCreateur"]),
                            Lieu = rdr["Lieu"].ToString(),
                            IdPole = Convert.ToInt16(rdr["IdPole"])
                        }
                    );
                }
                rdr.Close();
                cnn.Close();
                return reunionModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void CreateReunion(DateTime date, int idResponsable, int idCreateur, string lieu, int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"INSERT INTO reunion (Date, IdResponsable, IdCreateur, Lieu, IdPole) VALUES( " +
                    $"@Date, " +
                    $"@IdResponsable, " +
                    $"@IdCreateur," +
                    $"@Lieu, " +
                    $"@IdPole)";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@IdResponsable", idResponsable);
                cmd.Parameters.AddWithValue("@IdCreateur", idCreateur);
                cmd.Parameters.AddWithValue("@Lieu", lieu);
                cmd.Parameters.AddWithValue("@IdPole", idPole);

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
