using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class SellerDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public Seller Login(Seller seller)
        {
            Seller _seller = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select * from Sellers where user_name='" +
                        seller.Username + "' and user_pass='" +
                        Md5Algorithms.CreateMD5(seller.Password) + "';";
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        _seller = GetSeller(reader);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch{}
            }
            return _seller;
        }
        internal Seller GetSeller(MySqlDataReader reader)
        {
            Seller seller = new Seller();
            seller.SellerID = reader.GetInt32("seller_id");
            seller.SellerName = reader.GetString("seller_name");
            return seller;
        }
    }
}