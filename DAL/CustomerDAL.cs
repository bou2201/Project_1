using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CustomerDAL
    {
        private string query;
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlDataReader reader;
        public CustomerDAL() {}

        public Customer GetById(int customerId)
        {
            // Customer customer = new Customer();
            Customer customer = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    query = @"select customer_id, customer_name, customer_phone 
                        from Customers where customer_id=" + customerId + ";";
                    reader = (new MySqlCommand(query, connection)).ExecuteReader();
                    if (reader.Read())
                    {
                        customer = GetCustomer(reader);
                    }
                    reader.Close();
                }
                catch { }
                finally
                {
                    connection.Close();
                }
            }
            return customer;
        }

        internal Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.CustomerID = reader.GetInt32("customer_id");
            customer.CustomerName = reader.GetString("customer_name");
            customer.PhoneNumber = reader.GetString("customer_phone");
            return customer;
        }
    }
}