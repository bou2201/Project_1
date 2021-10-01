using System;
using Persistence;
using BL;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleAppPL
{
    class Program
    {
        private static Style style = new Style();
        static void Main(string[] args)
        {            
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Seller seller = new Seller();
            SellerBL bl = new SellerBL();
            ConsoleKey key;
            do
            {
                // Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
                Console.WriteLine(" ┌──────────────────────────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine(" │                               ╔╦╗┌─┐┌┐┌┌─┐┬ ┬┌─┐┌─┐┬─┐  ╔═╗┌┬┐┌─┐┬─┐┌─┐                          │");
                Console.WriteLine(" │                        ♦      ║║║├┤ │││└─┐│││├┤ ├─┤├┬┘  ╚═╗ │ │ │├┬┘├┤      ♦                    │");                
                Console.WriteLine(" │                               ╩ ╩└─┘┘└┘└─┘└┴┘└─┘┴ ┴┴└─  ╚═╝ ┴ └─┘┴└─└─┘                          │");                
                Console.WriteLine(" ╞──────────────────────────────────────────────────────────────────────────────────────────────────╡");                
                Console.WriteLine(" │                                           ♦     LOGIN     ♦                                      │");
                Console.WriteLine(" ╞──────────────────────────────────────────────────────────────────────────────────────────────────╡");
                Console.WriteLine(" │                                                                                                  │");
                Console.WriteLine(" │                       ┌──────────────────────────────────────────────────┐                       │");
                Console.WriteLine(" │                       │ ► Username :                                     │                       │");
                Console.WriteLine(" │                       └──────────────────────────────────────────────────┘                       │");
                Console.WriteLine(" │                                                                                                  │");
                Console.WriteLine(" │                       ┌──────────────────────────────────────────────────┐                       │");
                Console.WriteLine(" │                       │ ► Password :                                     │                       │");
                Console.WriteLine(" │                       └──────────────────────────────────────────────────┘                       │");
                Console.WriteLine(" │                                                                                                  │");
                Console.WriteLine(" │                                                                                                  │");
                Console.WriteLine(" │                                                                                                  │");
                Console.WriteLine(" │                                                                                                  │");
                Console.WriteLine(" └──────────────────────────────────────────────────────────────────────────────────────────────────┘");
                
                seller.Username = GetUsername();
                seller.Password = GetPassword();
                // seller.Username = "admin1";
                // seller.Password = "Menswear22@";
                                                               
                bool login = bl.Login(seller);
                if(login == false)
                {
                    Console.SetCursorPosition(35, 16);
                    style.TextColor(" ▲ Invalid Username Or Password !", ConsoleColor.Red);
                    Console.SetCursorPosition(26, 17);
                    style.TextColor(" ▲ Press Any Key To Continue Or 'Escape' To Exit !", ConsoleColor.Red);
                }
                else
                {
                    Menu menu = new Menu();
                    menu.MainMenu(seller);
                    break;
                }
                
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if(key == ConsoleKey.Escape){
                    break;
                }
                Console.WriteLine();
            } while (key != ConsoleKey.Escape);
        }

        static string GetUsername()
        {
            Console.SetCursorPosition(40, 9);
            var username = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && username.Length > 0)
                {
                    Console.Write("\b \b");
                    username = username[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write(keyInfo.KeyChar);
                    username += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return username;
        }

        static string GetPassword()
        {
            Console.SetCursorPosition(40, 13);
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
