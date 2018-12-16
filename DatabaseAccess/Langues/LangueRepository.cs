using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatabaseAccess.BDD;



namespace DatabaseAccess.Langue
{
    public class LangueRepository : ILangueRepository
    {
        public List<LangueModel> GetLangues()
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM langue";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rdrLangue = cmd.ExecuteReader();

                var listLangues = new List<LangueModel>();
                while (rdrLangue.Read())
                {
                    listLangues.Add(
                        new LangueModel()
                        {
                            IdLangue = Convert.ToInt16(rdrLangue["idLangue"]),
                            Nom = rdrLangue["Nom"].ToString()
                        }
                    );
                }
                rdrLangue.Close();
                cnn.Close();
                return listLangues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LangueModel GetLangue(int idLangue)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM langue WHERE idLangue=@idLangue";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idLangue", idLangue);
                MySqlDataReader rdrLangue = cmd.ExecuteReader();
                LangueModel Langue = null;
                if (rdrLangue.Read())
                {
                    Langue = new LangueModel()
                    {
                        IdLangue = Convert.ToInt16(rdrLangue["idLangue"]),
                        Nom = rdrLangue["Nom"].ToString()
                    };
                }
                rdrLangue.Close();
                cnn.Close();
                if (Langue == null)
                {
                    throw new Exception("Cette langue n'existe pas");
                }
                return Langue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditLangue(int IdLangue, string Nom)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"UPDATE langue SET " +
                    $"Nom = @Nom " +
                    $"WHERE idLangue=@idLangue";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Nom", Nom);
                cmd.Parameters.AddWithValue("@idLangue", IdLangue);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteLangue(int IdLangue)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"Delete from langue " +
                    $"WHERE idLangue=@idLangue";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idLangue", IdLangue);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateLangue(string nom)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"INSERT INTO langue (Nom) VALUES(@nom)";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@nom", nom);

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
