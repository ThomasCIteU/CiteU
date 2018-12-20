using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatabaseAccess.BDD;



namespace DatabaseAccess.Pole
{
    public class PoleRepository : IPoleRepository
    {
        public List<PoleModel> GetPoles()
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM pole";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rdrPole = cmd.ExecuteReader();

                var listPoles = new List<PoleModel>();
                while (rdrPole.Read())
                {
                    listPoles.Add(
                        new PoleModel()
                        {   
                            IdPole = Convert.ToInt16(rdrPole["idPole"]),
                            Libelle = rdrPole["Libelle"].ToString(),
                            IdResponsable = Convert.ToInt16(rdrPole["IdResponsable"]),
                            IdAdjoint = Convert.ToInt16(rdrPole["IdAdjoint"]),
                        }
                    );
                }
                rdrPole.Close();
                cnn.Close();
                return listPoles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PoleModel GetPole(int idPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM pole WHERE idPole=@idPole";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idPole", idPole);
                MySqlDataReader rdrPole = cmd.ExecuteReader();
                PoleModel Pole = null;
                if (rdrPole.Read())
                {
                    Pole = new PoleModel()
                    {
                        IdPole = Convert.ToInt16(rdrPole["idPole"]),
                        Libelle = rdrPole["Libelle"].ToString(),
                        IdResponsable = Convert.ToInt16(rdrPole["IdResponsable"]),
                        IdAdjoint = Convert.ToInt16(rdrPole["IdAdjoint"]),
                    };
                }
                rdrPole.Close();
                cnn.Close();
                if (Pole == null)
                {
                    throw new Exception("Ce pôle n'existe pas");
                }
                return Pole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetPoleFromUser(int idUser)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT idPole FROM user u left join assemblee a on a.idassemblee = u.Assemblee where idUser = @idUser";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idUser", idUser);
                MySqlDataReader rdrPole = cmd.ExecuteReader();
                int pole = 0;
                if (rdrPole.Read())
                {
                    pole = Convert.ToInt16(rdrPole["idPole"]);
                }
                rdrPole.Close();
                cnn.Close();
                if (pole == 0)
                {
                    throw new Exception("Impossible de retrouver le pôle de l'utilisateur");
                }
                return pole;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditPole(int IdPole, string Libelle, int idResponsable, int idAdjoint)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"UPDATE pole SET " +
                    $"Libelle = @Libelle, " +
                    $"IdResponsable = @IdResponsable, " +
                    $"IdAdjoint = @IdAdjoint " +
                    $"WHERE idPole=@idPole";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Libelle", Libelle);
                cmd.Parameters.AddWithValue("@IdResponsable", idResponsable);
                cmd.Parameters.AddWithValue("@IdAdjoint", idAdjoint);
                cmd.Parameters.AddWithValue("@idPole", IdPole);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeletePole(int IdPole)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"Delete from pole " +
                    $"WHERE idPole=@idPole";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idPole", IdPole);

                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreatePole(string libelle, int idResponsable, int idAdjoint)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = $"INSERT INTO pole (Libelle, IdResponsable, IdAdjoint) VALUES( " +
                    $"@Libelle, " +
                    $"@IdResponsable, " +
                    $"@IdAdjoint)";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@Libelle", libelle);
                cmd.Parameters.AddWithValue("@IdResponsable", idResponsable);
                cmd.Parameters.AddWithValue("@IdAdjoint", idAdjoint);

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
