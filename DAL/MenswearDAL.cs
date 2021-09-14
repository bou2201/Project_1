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
            try
            {
                connection.Open();
                query = @"select menswear_id, menswear_name, ifnull(menswear_description, '') as menswear_description,
                        menswear_brand, menswear_material, menswear_price, category_id
                        from Menswears where menswear_id=@menswearID;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@menswearID", menswearID);
                MySqlDataReader reader = command.ExecuteReader();
                var readerRead = reader.Read();
                if (readerRead)
                {
                    menswear = GetMenswear(reader);
                }
                reader.Close();
            }
            catch {}
            finally
            {
                connection.Close();
            }
            return menswear;
        }
        public Menswear GetMenswear(MySqlDataReader reader)
        {
            Menswear menswear = new Menswear();
            menswear.MenswearID = reader.GetInt32("menswear_id");
            menswear.MenswearName = reader.GetString("menswear_name");
            menswear.Description = reader.GetString("menswear_description");
            menswear.Brand = reader.GetString("menswear_brand");
            menswear.Material = reader.GetString("menswear_material");
            menswear.Price = reader.GetDouble("menswear_price");
            menswear.MenswearCategory = new Category();
            menswear.MenswearCategory.CategoryID = reader.GetInt32("category_id");
            return menswear;
        }
        public List<Menswear> SearchByName(int menswearFilter, Menswear menswear)
        {
            List<Menswear> menswears = null;
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
            return menswears;
        }
    }
}