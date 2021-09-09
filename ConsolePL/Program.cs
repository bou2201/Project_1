using System;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Username: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string pass = GetPassword();
            Console.WriteLine();
            
            Seller seller = new Seller() { Username = userName, Password = pass };
            SellerBL bl = new SellerBL();
            bool login = bl.Login(seller);
            if (login == false)
            {
                Console.WriteLine("Can't Login");
            }
            else
            {
                Console.WriteLine("Logged in");
                Menu.LoginMenu();
            }
        }

        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }       
    }
}
