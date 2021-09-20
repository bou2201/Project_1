using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;

namespace DAL
{
    public class InvoiceDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public bool CreateInvoice(Invoice invoice)
        {
            if(invoice == null || invoice.ListMenswear == null || invoice.ListMenswear.Count == 0)
            {
                return false;
            }
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                //Lock update all tables
                command.CommandText = "lock tables Customers write, Invoices write, Menswears write, InvoiceDetails write;";
                command.ExecuteNonQuery();
                MySqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                MySqlDataReader reader = null;
                if(invoice.CustomerInfo == null || invoice.CustomerInfo.CustomerName == null || invoice.CustomerInfo.CustomerName == "")
                {
                    //set default customer with customer id = 1
                    invoice.CustomerInfo = new Customer() {CustomerID = 1};
                }
                try
                {
                    if(invoice.CustomerInfo.CustomerID == null)
                    {
                        //Insert new Customer
                        command.CommandText = @"insert into Customers(customer_name, customer_phone)
                            values ('" + invoice.CustomerInfo.CustomerName + "','" + (invoice.CustomerInfo.PhoneNumber ?? "") + "');";
                        command.ExecuteNonQuery();
                        //Get new customer id
                        command.CommandText = "select customer_id from Customers order by customer_id desc limit 1;";
                        reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            invoice.CustomerInfo.CustomerID = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                    }
                    else
                    {
                        //get Customer by Id
                        command.CommandText = "select * from Customers where customer_id=@customerId;";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@customerId", invoice.CustomerInfo.CustomerID);
                        reader = command.ExecuteReader();
                        if(reader.Read())
                        {
                            invoice.CustomerInfo = new CustomerDAL().GetCustomer(reader);
                        }
                        reader.Close();
                    }
                    if(invoice.CustomerInfo == null || invoice.CustomerInfo.CustomerID == null) 
                    {
                        throw new Exception("Can't find Customer !!!");
                    }
                    //Insert Order
                    command.CommandText = "insert into Orders(customer_id, order_status) values (@customerId, @invoiceStatus);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@customerId", invoice.CustomerInfo.CustomerID);
                    command.Parameters.AddWithValue("@invoiceStatus", InvoiceStatus.CREATE_NEW_INVOICE);
                    command.ExecuteNonQuery();
                    //get new Invoice_no
                    command.CommandText = "select LAST_INSERT_ID() as invoice_no";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        invoice.InvoiceNo = reader.GetInt32("invoice_no");
                    }
                    reader.Close();

                    //insert Invoice Details table
                    foreach(var menswear in invoice.ListMenswear)
                    {
                        if(menswear.MenswearID == null || menswear.Amount <= 0)
                        {
                            throw new Exception("Not Exist Menswear");
                        }
                        //get menswear_price
                        command.CommandText = "select menswear_id from Menswears where menswear_id=@menswearId;";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@menswearId", menswear.MenswearID);
                        reader = command.ExecuteReader();
                        if(!reader.Read())
                        {
                            throw new Exception("Not Exist Menswear");
                        }
                        menswear.Price = reader.GetDecimal("menswear_price");
                        reader.Close();

                        //insert to Invoice Details
                        command.CommandText = @"insert into InvoiceDetails(invoice_no, menswear_id, menswear_price, quantity) values 
                            (" + invoice.InvoiceNo + ", " + menswear.MenswearID + ", " + menswear.Price + ", " + menswear.Amount + ");";
                        command.ExecuteNonQuery();

                        //update amount in Menswears
                        command.CommandText = "update Menswears set amount=amount-@quantity where menswear_id=" + menswear.MenswearID + ";";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@quantity", menswear.Amount);
                        command.ExecuteNonQuery();
                    }
                    //commit transaction
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch {}
                }
                finally
                {
                    // unlock all table
                    command.CommandText = "unlock tables";
                    command.ExecuteNonQuery();
                }
            }
            catch {}
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}