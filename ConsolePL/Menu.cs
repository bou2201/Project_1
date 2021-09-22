using System;
using System.Collections.Generic;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    public class Menu
    {
        public static void GetLineEqual()
        {
            Console.WriteLine("=========================================");
        }
        public static void GetLineDash()
        {
            Console.WriteLine("-----------------------------------------");
        }
        public static void MainMenu()
        {
            int choice, choose;
            MenswearBL menswearBL = new MenswearBL();
            InvoiceBL invoiceBL = new InvoiceBL();
            CustomerBL customerBL = new CustomerBL();
            List<Menswear> menswears;
            // List<MenswearTable> menswearTables = new List<MenswearTable>();
            string title = "|\t\tMAIN MENU\t\t|";
            string[] mainMenu = new string[] { "SEARCH", "EXIT" };
            string[] searchMenu = new string[] { "SEARCH MENSWEAR BY ID", "SEARCH MENSWEAR BY NAME", "SHOW ALL MENSWEAR", "BACK TO MAIN MENU" };

            while (true)
            {
                choice = tableMenu(title, mainMenu);
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        do
                        {
                            choose = tableMenu("|\t\t  SEARCH\t\t|", searchMenu);
                            Console.Clear();
                            switch (choose)
                            {
                                case 1:
                                    Console.Write("\nInput Menswear ID: ");
                                    int id;
                                    try
                                    {
                                        if (Int32.TryParse(Console.ReadLine(), out id))
                                        {
                                            Menswear mens = menswearBL.SearchByID(id);
                                            if (mens != null)
                                            {
                                                GetLineDash();
                                                Console.WriteLine("|\t     MENSWEAR DETAILS\t\t|");
                                                GetLineDash();
                                                Console.WriteLine($" Menswear ID: {mens.MenswearID}");
                                                Console.WriteLine($" Menswear Name: {mens.MenswearName}");
                                                Console.WriteLine($" Description: {mens.Description}");
                                                Console.WriteLine($" Brand: {mens.Brand}");
                                                Console.WriteLine($" Material: {mens.Material}");
                                                Console.WriteLine($" Price: {mens.Price}");
                                                Console.WriteLine($" Category: {mens.MenswearCategory.CategoryName}");                                                
                                                Console.WriteLine($" Color: {mens.ColorSizeList.ColorID.ColorName}");
                                                Console.WriteLine($" Size: {mens.ColorSizeList.SizeID.SizeName}");
                                                Console.WriteLine($" Quantity: {mens.ColorSizeList.Quantity}");                                                
                                                GetLineDash();
                                            }
                                            else
                                            {
                                                GetLineDash();
                                                Console.WriteLine($"ID: {id} doesn't exist");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid ID!");
                                        }
                                        Console.Write("Press any key to back search menu ...");
                                        Console.ReadLine();
                                        Console.Clear();
                                    }
                                    catch (Exception e) { Console.WriteLine(e); }
                                    break;
                                case 2:
                                    Console.Write("\nInput Name: ");
                                    string str = Console.ReadLine();
                                    menswears = menswearBL.SearchByName(str);
                                    ShowListMenswear(menswears);
                                    break;
                                case 3:
                                    menswears = menswearBL.GetAll();
                                    ShowListMenswear(menswears);
                                    int input;
                                    do
                                    {
                                        Console.Write("Enter '1' to order, '2' to exit");
                                        int.TryParse(Console.ReadLine(), out input);
                                        switch (input)
                                        {
                                            case 1:
                                                Invoice invoice = new Invoice();
                                                Customer customer = new Customer();
                                                Console.Write("\nInput Menswear ID: ");
                                                int mensId;
                                                if (int.TryParse(Console.ReadLine(), out mensId))
                                                {
                                                    Menswear mens = menswearBL.SearchByID(mensId);
                                                    if (mens == null)
                                                    {
                                                        Console.WriteLine($"ID: {mensId} doesn't exist");
                                                    }
                                                    else
                                                    {                                                        
                                                        Console.Write("\nInput Quantity: ");
                                                        mens.Amount = int.Parse(Console.ReadLine());
                                                        
                                                    }
                                                }
                                                break;
                                            case 2:
                                                break;
                                            default:
                                                Console.WriteLine("Invalid choice!");
                                                break;
                                        }
                                    } while (input != 2);
                                    break;
                                case 4:
                                    break;
                                default:
                                    Console.WriteLine("Invalid choice!");
                                    break;
                            }
                        } while (choose != 4);
                        break;
                    case 2:
                        break;
                    default:
                        Console.WriteLine("Invalid choice !");
                        break;
                }
                if (choice == 2) { break; }
            }
        }

        static void ShowListMenswear(List<Menswear> menswears)
        {
            if (menswears.Count == 0)
            {
                Console.WriteLine("This name doesn't exist in the system !!!");
                GetLineDash();
            }
            else if (menswears != null)
            {
                GetLineDash();
                Console.WriteLine("|\t     LIST MENSWEAR\t\t|");
                GetLineDash();
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("| {0, -5} | {1, -25} | {2, -10} |", "ID", "Name", "Price(VND)");
                Console.WriteLine("--------------------------------------------------");
                foreach (var list in menswears)
                {
                    Console.WriteLine("| {0, -5} | {1, -25} | {2, -10} |", list.MenswearID, list.MenswearName, list.Price);
                }
                Console.WriteLine("--------------------------------------------------");
            }
            Console.Write("Press any key to back search menu ...");
            Console.ReadLine();
            Console.Clear();
        }

        static int tableMenu(string title, string[] menu)
        {
            int choose = 0;
            int lenghtMenu = menu.Length;
            GetLineDash();
            Console.WriteLine(title);
            GetLineDash();
            for (int i = 0; i < lenghtMenu; i++)
            {
                Console.WriteLine($" {i + 1}. {menu[i]}");
            }
            GetLineDash();
            do
            {
                Console.Write("Choose: ");
                int.TryParse(Console.ReadLine(), out choose);
            } while (choose <= 0 && choose > lenghtMenu);
            return choose;
        }
    }
}