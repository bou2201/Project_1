using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class SellerDAL
    {
        MySqlConnection connection = DbHelper.GetConnection();
        public bool Login(Seller seller)
        {
            bool check = false;
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
                        check = true;
                    }
                    reader.Close();
                    connection.Close();                   
                }
                catch
                {
                    check = false;
                }
            }
            return check;
        }
    }
}