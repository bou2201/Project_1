using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class SellerDAL
    {
        public int Login(Seller seller)
        {
            int login = 0;
            try
            {
                MySqlConnection connection = DbHelper.GetConnection();
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from Sellers where user_name='" + 
                    seller.Username + "' and user_pass='" + 
                    Md5Algorithms.CreateMD5(seller.Password) + "';";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    login = 1;
                }
                else
                {
                    login = 0;
                }
                reader.Close();
                connection.Close();
            }
            catch
            {
                login = -1;
            }
            return login;
        }
    }
}