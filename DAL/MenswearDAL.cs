using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;

namespace DAL
{
    public static class MenswearFilter
    {
        public const int GET_ALL = 0;
        public const int FILTER_BY_NAME = 1;
    }
    public class MenswearDAL
    {
        private string query;
        private MySqlConnection connection = DbConfig.GetConnection();

        public Menswear SearchByID(int menswearID)
        {
            Menswear menswear = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    query = @"select * from Menswears, MenswearTables, Categories, Colors, Sizes
                                where Menswears.menswear_id = @menswearID 
                                and Menswears.menswear_id = MenswearTables.menswear_id
								and Menswears.category_id = Categories.category_id
                                and MenswearTables.color_id = Colors.color_id
                                and MenswearTables.size_id = Sizes.size_id;";                   
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@menswearID", menswearID);
                    MySqlDataReader reader = command.ExecuteReader();
                    var readerRead = reader.Read();
                    if (readerRead)
                    {
                        menswear = GetMenswearDetails(reader);
                        menswear.ColorSizeList = new MenswearTable()
                        {
                            ColorID = new Color()
                            {
                                ColorID = reader.GetInt32("color_id"),
                                ColorName = reader.GetString("color_name")
                            },
                            SizeID = new Size()
                            {
                                SizeID = reader.GetInt32("size_id"),
                                SizeName = reader.GetString("size_name")
                            },
                            Quantity = reader.GetInt32("quantity")            
                        };
                        
                    }
                    reader.Close();
                }
                catch { }
                finally
                {
                    connection.Close();
                }
            }
            return menswear;
        }
        internal Menswear GetMenswearDetails(MySqlDataReader reader)
        {
            Menswear menswear = new Menswear();
            menswear.MenswearID = reader.GetInt32("menswear_id");
            menswear.MenswearName = reader.GetString("menswear_name");
            menswear.Description = reader.GetString("menswear_description");
            menswear.Brand = reader.GetString("menswear_brand");
            menswear.Material = reader.GetString("menswear_material");
            menswear.Price = reader.GetDecimal("menswear_price");
            menswear.MenswearCategory = new Category()
            {                
                CategoryID = reader.GetInt32("category_id"),
                CategoryName = reader.GetString("category_name")
            };            
            return menswear;
        }
        internal Menswear GetMenswear(MySqlDataReader reader)
        {
            Menswear menswear = new Menswear();
            menswear.MenswearID = reader.GetInt32("menswear_id");
            menswear.MenswearName = reader.GetString("menswear_name");
            menswear.Description = reader.GetString("menswear_description");
            menswear.Brand = reader.GetString("menswear_brand");
            menswear.Material = reader.GetString("menswear_material");
            menswear.Price = reader.GetDecimal("menswear_price");
            menswear.MenswearCategory = new Category();
            menswear.MenswearCategory.CategoryID = reader.GetInt32("category_id");
            return menswear;
        }
        public List<Menswear> SearchByName(int menswearFilter, Menswear menswear)
        {
            List<Menswear> menswears = null;
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("", connection);
                    switch (menswearFilter)
                    {
                        case MenswearFilter.GET_ALL:
                            query = @"select menswear_id, menswear_name, menswear_material, menswear_brand, menswear_price, category_id,
                                ifnull(menswear_description, '') as menswear_description
                                from Menswears";
                            
                            break;
                        case MenswearFilter.FILTER_BY_NAME:
                            query = @"select menswear_id, menswear_name, menswear_material, menswear_brand, menswear_price, category_id,
                                ifnull(menswear_description, '') as menswear_description from Menswears
                                where menswear_name like concat('%',@menswearID,'%');";
                            command.Parameters.AddWithValue("@menswearID", menswear.MenswearName);
                            break;
                    }
                    command.CommandText = query;
                    MySqlDataReader reader = command.ExecuteReader();
                    menswears = new List<Menswear>();
                    while (reader.Read())
                    {
                        menswears.Add(GetMenswear(reader));
                    }
                    reader.Close();

                }
                catch { }
                finally
                {
                    connection.Close();
                }
            }
            return menswears;
        }

        public List<Menswear> GetPriceByID(int invoiceId)
        {
            var menswears = new List<Menswear>();
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("", connection);
                var query = @$"select * from InvoiceDetails id 
                            left join Menswears ms 
                            on id.menswear_id = ms.menswear_id 
                            where id.invoice_no = {invoiceId};";
                command.CommandText = query;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    menswears.Add(GetPrice(reader));
                }
                reader.Close();
            }
            catch {}
            finally
            {
                connection.Close();
            }
            return menswears;
        }

        internal Menswear GetPrice(MySqlDataReader reader)
        {
            Menswear newMenswear = new Menswear();
            newMenswear.MenswearID =  reader.GetInt32("menswear_id");
            newMenswear.Quantity =  reader.GetInt32("quantity");
            newMenswear.Price = reader.GetDecimal("menswear_price");
            return newMenswear;
        }
    }
}