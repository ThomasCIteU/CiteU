using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAccess.BDD
{
    public static class BDDRepository
    {
        public static MySqlConnection OpenConnexion()
        {
            string connetionString = null;
            MySqlConnection cnn;
            connetionString = "server=127.0.0.1;database=citeu;uid=root;pwd=5^6n=Z^HEQArnKvF;";
            cnn = new MySqlConnection(connetionString);
            try
            {
                cnn.Open();
                return cnn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CloseConnexion(MySqlConnection cnn)
        {
            try
            {
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
