using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatabaseAccess.BDD;



namespace DatabaseAccess.Assemblee
{
    public class AssembleeRepository : IAssembleeRepository
    {
        public List<AssembleeModel> GetAssemblees()
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM assemblee";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                var listAssembblee = new List<AssembleeModel>();
                while (rdr.Read())
                {
                    listAssembblee.Add(
                        new AssembleeModel()
                        {
                            IdAssemblee = Convert.ToInt16(rdr["idassemblee"]),
                            Nom = rdr["Nom"].ToString(),
                            IdPole = Convert.ToInt16(rdr["IdPole"])
                        }
                    );
                }
                rdr.Close();

                cnn.Close();
                return listAssembblee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AssembleeModel GetAssemblee(int idAssemblee)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM assemblee WHERE idAssemblee=@idAssemblee";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idAssemblee", idAssemblee);
                MySqlDataReader rdrAssemblee = cmd.ExecuteReader();
                AssembleeModel Assemblee = null;
                if (rdrAssemblee.Read())
                {
                    Assemblee = new AssembleeModel()
                    {
                        IdAssemblee = Convert.ToInt16(rdrAssemblee["idAssemblee"]),
                        Nom = rdrAssemblee["Nom"].ToString(),
                        IdPole = Convert.ToInt16(rdrAssemblee["IdPole"])
                    };
                }
                rdrAssemblee.Close();

                cnn.Close();
                if (Assemblee == null)
                {
                    throw new Exception("Cette assemblée n'existe pas");
                }
                return Assemblee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditAssemblee(int IdAssemblee, string nom, int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"UPDATE assemblee SET " +
                    $"Nom = @Nom, " +
                    $"IdPole = @idPole " +
                    $"WHERE idassemblee=@IdAssemblee";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@IdPole", idPole);
                cmd.Parameters.AddWithValue("@IdAssemblee", IdAssemblee);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAssemblee(int IdAssemblee)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"Delete from assemblee " +
                    $"WHERE idassemblee=@IdAssemblee";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@IdAssemblee", IdAssemblee);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateAssemblee(string nom, int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"INSERT INTO assemblee (Nom, IdPole) VALUES( " +
                    $"@Nom, " +
                    $"@IdPole)";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Nom", nom);
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
