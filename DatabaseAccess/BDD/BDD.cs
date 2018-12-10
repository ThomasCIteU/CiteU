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
            //Connexion Locale
            //connetionString = "server=127.0.0.1;database=citeu;uid=root;pwd=5^6n=Z^HEQArnKvF;";
            //Connexion host gratuit
            //connetionString = "Server=MYSQL6002.site4now.net;Database=db_a435d6_citeu;Uid=a435d6_citeu;Pwd=Kanabeach1997";
            //Connexion myalwaysdata            
            connetionString = "Server=MYSQL6002.site4now.net;Database=db_a435d6_citeu;Uid=a435d6_citeu;Pwd=Kanabeach1997";
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
    }
}
