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

        public List<ReunionModel> GetAllReunions(int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM reunion WHERE IdPole = @idPole";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idPole", idPole);
                MySqlDataReader rdr = cmd.ExecuteReader();

                var reunionModel = new List<ReunionModel>();
                while (rdr.Read())
                {
                    reunionModel.Add(
                        new ReunionModel()
                        {
                            IdReunion = Convert.ToInt16(rdr["idreunion"]),
                            Date = Convert.ToDateTime(rdr["Date"]),
                            IdResponsable = Convert.ToInt16(rdr["IdResponsable"]),
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

        public void EditReunion(int idReunion, DateTime date, int idResponsable, int idCreateur, string lieu, int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"UPDATE Reunion SET " +
                    $"Date = @Date, " +
                    $"IdResponsable = @IdResponsable, " +
                    $"IdCreateur = @IdCreateur," +
                    $"Lieu = @Lieu, " +
                    $"IdPole = @IdPole " +
                    $"WHERE idreunion=@idReunion";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@IdResponsable", idResponsable);
                cmd.Parameters.AddWithValue("@IdCreateur", idCreateur);
                cmd.Parameters.AddWithValue("@Lieu", lieu);
                cmd.Parameters.AddWithValue("@IdPole", idPole);
                cmd.Parameters.AddWithValue("@idReunion", idReunion);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteReunion(int IdReunion)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"Delete from reunion " +
                    $"WHERE idReunion=@idReunion";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idReunion", IdReunion);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ReunionModel GetReunion(int idReunion)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM reunion WHERE idReunion=@idReunion";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idReunion", idReunion);
                MySqlDataReader rdr = cmd.ExecuteReader();
                ReunionModel Reunion = null;
                if (rdr.Read())
                {
                    Reunion = new ReunionModel()
                    {
                        IdReunion = Convert.ToInt16(rdr["idreunion"]),
                        Date = Convert.ToDateTime(rdr["Date"]),
                        IdResponsable = Convert.ToInt16(rdr["IdResponsable"]),
                        IdCreateur = Convert.ToInt16(rdr["IdCreateur"]),
                        Lieu = rdr["Lieu"].ToString(),
                        IdPole = Convert.ToInt16(rdr["IdPole"])
                    };
                }
                rdr.Close();
                cnn.Close();
                if (Reunion == null)
                {
                    throw new Exception("Ce pôle n'existe pas");
                }
                return Reunion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ReunionModel GetReunion(DateTime date)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM reunion WHERE CAST(Date AS DATE) =@date";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                MySqlDataReader rdr = cmd.ExecuteReader();
                ReunionModel Reunion = null;
                if (rdr.Read())
                {
                    Reunion = new ReunionModel()
                    {
                        IdReunion = Convert.ToInt16(rdr["idreunion"]),
                        Date = Convert.ToDateTime(rdr["Date"]),
                        IdResponsable = Convert.ToInt16(rdr["IdResponsable"]),
                        IdCreateur = Convert.ToInt16(rdr["IdCreateur"]),
                        Lieu = rdr["Lieu"].ToString(),
                        IdPole = Convert.ToInt16(rdr["IdPole"])
                    };
                }
                rdr.Close();
                cnn.Close();
                if (Reunion == null)
                {
                    return null;
                }
                return Reunion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReunionModel GetReunion(DateTime date, int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM reunion WHERE CAST(Date AS DATE) =@date AND IdPole = @idPole";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idPole", idPole);
                MySqlDataReader rdr = cmd.ExecuteReader();
                ReunionModel Reunion = null;
                if (rdr.Read())
                {
                    Reunion = new ReunionModel()
                    {
                        IdReunion = Convert.ToInt16(rdr["idreunion"]),
                        Date = Convert.ToDateTime(rdr["Date"]),
                        IdResponsable = Convert.ToInt16(rdr["IdResponsable"]),
                        IdCreateur = Convert.ToInt16(rdr["IdCreateur"]),
                        Lieu = rdr["Lieu"].ToString(),
                        IdPole = Convert.ToInt16(rdr["IdPole"])
                    };
                }
                rdr.Close();
                cnn.Close();
                if (Reunion == null)
                {
                    return null;
                }
                return Reunion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
