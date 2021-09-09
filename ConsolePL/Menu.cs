using System;

namespace ConsoleAppPL
{
    public class Menu
    {
        public static void LoginMenu()
        {
            {
                string title = "title";
                string[] menu = { "Show", "Exit" };
                string[] menu2 = { "Search", "Exit" };
                int choose = Menu1(title, menu);
                int choose2;
                switch (choose)
                {
                    case 1:
                        choose2 = Menu1(title, menu2);
                        break;
                    case 2:
                        break;
                }
            }
        }
        static int Menu1(string title, string[] menu)
        {
            int choose = 0;
            string line = "==================================";
            Console.WriteLine(line);
            Console.WriteLine(" " + title);
            Console.WriteLine(line);
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($" {i + 1}. {menu[i]}");
            }
            Console.WriteLine(line);
            do
            {
                Console.Write("Choose: ");
                int.TryParse(Console.ReadLine(), out choose);
            } while (choose <= 0 && choose > menu.Length);
            return choose;
        }
    }
}