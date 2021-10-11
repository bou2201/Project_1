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
            SellerBL sellerBL = new SellerBL();
            Style style = new Style();
            ConsoleKey key;
            do
            {
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
                Console.WriteLine(" │                       │ ► Username:                                      │                       │");
                Console.WriteLine(" │                       └──────────────────────────────────────────────────┘                       │");
                Console.WriteLine(" │                                                                                                  │");
                Console.WriteLine(" │                       ┌──────────────────────────────────────────────────┐                       │");
                Console.WriteLine(" │                       │ ► Password:                                      │                       │");
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
                                                               
                Seller _seller = sellerBL.Login(seller);
                if(_seller == null)
                {
                    Console.SetCursorPosition(35, 16);
                    style.TextColor(" ▲ Invalid Username Or Password !", ConsoleColor.Red);
                    Console.SetCursorPosition(26, 17);
                    style.TextColor(" ▲ Press Any Key To Continue Or 'Escape' To Exit !", ConsoleColor.Red);
                }
                else
                {
                    Menu menu = new Menu();
                    menu.SellerMenu(_seller);
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
            Console.SetCursorPosition(39, 9);
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
            Console.SetCursorPosition(39, 13);
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
