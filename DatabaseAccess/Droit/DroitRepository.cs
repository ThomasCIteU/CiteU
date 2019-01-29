using DatabaseAccess.BDD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.Droit
{
    public class DroitRepository : IDroitRepository
    {
        public DroitModel getDroit(int idDroit)
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM droit WHERE idDroit=@idDroit";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@idDroit", idDroit);
                MySqlDataReader rdrDroit = cmd.ExecuteReader();
                DroitModel Droit = null;
                if (rdrDroit.Read())
                {
                    Droit = new DroitModel()
                    {
                        IdDroit = Convert.ToInt16(rdrDroit["idDroit"]),
                        Libelle = rdrDroit["LibelleDroit"].ToString()
                    };
                }
                rdrDroit.Close();
                cnn.Close();
                if (Droit == null)
                {
                    throw new Exception("Ce Droit n'existe pas");
                }
                return Droit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DroitModel> getAllDroits()
        {
            MySqlConnection cnn = BDDRepository.OpenConnexion();
            try
            {
                string sql = "SELECT * FROM droit where idDroit <> 0 ";
                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                MySqlDataReader rdrDroit = cmd.ExecuteReader();

                var listDroits = new List<DroitModel>();
                while (rdrDroit.Read())
                {
                    listDroits.Add(
                        new DroitModel()
                        {
                            IdDroit = Convert.ToInt16(rdrDroit["idDroit"]),
                            Libelle = rdrDroit["LibelleDroit"].ToString()
                        }
                    );
                }
                rdrDroit.Close();
                cnn.Close();
                return listDroits;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
