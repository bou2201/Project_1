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
            //valid username password here
            Seller seller = new Seller() { Username = userName, Password = pass };
            SellerBL bl = new SellerBL();
            int login = bl.Login(seller);
            if (login <= 0)
            {
                Console.WriteLine("Can't Login");
            }
            else
            {
                Console.WriteLine("Wellcome to System...");
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
