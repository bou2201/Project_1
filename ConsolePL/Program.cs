using System;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Seller seller = new Seller();
            SellerBL bl = new SellerBL();
            while (true)
            {
                Menu.GetLineDash();
                Console.WriteLine("|\t\t LOGIN\t\t\t|");
                Menu.GetLineDash();
                Console.Write("Username: ");
                seller.Username = Console.ReadLine();
                Console.Write("Password: ");
                seller.Password = GetPassword();
                Console.WriteLine();
                bool login = bl.Login(seller);
                string input;

                if (login == false)
                {
                    Menu.GetLineDash();
                    Console.WriteLine("INVALID USERNAME OR PASSWORD !");
                    while (true)
                    {
                        Console.Write("Input 1 to continue re-enter or input 0 to exit: ");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1": 
                                break;
                            case "0": 
                                return;
                            default:
                                Console.WriteLine("Invalid choice !");
                                break;
                        }
                        if (input == "1")
                        {
                            break;
                        }
                    }
                }
                else
                {
                    Menu.GetLineDash();
                    Console.WriteLine("LOGIN TO THE SYSTEM SUCCESSFULLY !");
                    Menu.MainMenu();
                    return;
                }
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
