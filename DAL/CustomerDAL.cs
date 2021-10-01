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
        public CustomerDAL() { }

        public Customer GetById(int customerId)
        {
            Customer customer = new Customer();
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

        public Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.CustomerID = reader.GetInt32("customer_id");
            customer.CustomerName = reader.GetString("customer_name");
            customer.PhoneNumber = reader.GetString("customer_phone");
            return customer;
        }

        public int? AddCustomer(Customer customer)
        {
            int? result = null;
            lock (connection)
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                MySqlCommand command = new MySqlCommand("", connection);
                try
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customerName", customer.CustomerName);
                    command.Parameters["@customerName"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                    command.Parameters["@phoneNumber"].Direction = System.Data.ParameterDirection.Input;
                    command.Parameters.AddWithValue("@customerId", MySqlDbType.Int32);
                    command.Parameters["@customerId"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    result = (int)command.Parameters["@customerId"].Value;
                }
                catch { }
                finally
                {
                    connection.Close();
                }
            }
            return result;
        }
    }
}