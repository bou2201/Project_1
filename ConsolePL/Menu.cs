using System;
using System.Collections.Generic;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    public class Menu
    {
        private static Style style = new Style();

        public void MainMenu(Seller seller)
        {
            string choice = string.Empty;
            List<Menswear> menswears;
            MenswearBL menswearBL = new MenswearBL();
            InvoiceBL invoiceBL = new InvoiceBL();
            CustomerBL customerBL = new CustomerBL();
            menswears = menswearBL.GetAll();
            choice = ShowMenswear(menswears);
            do
            {
                switch (choice.ToUpper())
                {
                    case "ESCAPE":
                        break;
                    case "A"://search by id
                        style.WriteAt("► Input Menswear ID: ", 8, 30);
                        int id;
                        try
                        {
                            if (Int32.TryParse(Console.ReadLine(), out id))
                            {
                                Menswear mens = menswearBL.SearchByID(id);
                                if (mens != null)
                                {
                                    ShowDetails(mens);
                                }
                                else
                                {
                                    Console.SetCursorPosition(8, 31);
                                    style.TextColor($"▲ ID: {id} doesn't exist !", ConsoleColor.Red);
                                }
                            }
                            else
                            {
                                Console.SetCursorPosition(8, 31);
                                style.TextColor("▲ Invalid ID !", ConsoleColor.Red);
                            }
                            Console.ReadLine();
                            Console.Clear();
                        }
                        catch { }
                        choice = "ALL";
                        break;
                    case "B":
                        // search by name
                        style.WriteAt("► Input Name: ", 8, 30);
                        choice = style.ReadString();
                        menswears = menswearBL.SearchByName(choice);
                        choice = ShowMenswear(menswears);
                        break;
                    case "ALL":
                        choice = ShowMenswear(menswearBL.GetAll());
                        break;
                    default:
                        //--> details
                        int idMens;
                        int.TryParse(choice, out idMens);
                        ShowDetails(menswearBL.SearchByID(idMens));
                        Console.ReadLine();
                        break;
                }
            } while (choice != "Escape");
        }
        public string ShowMenswear(List<Menswear> menswears)
        {
            if (menswears.Count == 0)
            {
                Console.SetCursorPosition(8, 30);
                style.TextColor("▲ Not Exist Any Menswear !", ConsoleColor.Red);
                Console.ReadLine();
                return "ALL";
            }
            string choice = string.Empty;
            Dictionary<int, int> IndexID = new Dictionary<int, int>();
            List<Page> pages = new List<Page>();
            Page page = new Page();
            int count = 0;
            int max_page = (menswears.Count % 15 == 0) ? (menswears.Count / 15) : (menswears.Count / 15 + 1);
            int count_menswears = menswears.Count;
            int line = 0;
            int menswear_no = (int)menswears[count].MenswearID;
            int page_number = 1;
            while (count < count_menswears)
            {
                if (line == 0)
                {
                    page = new Page();
                    menswear_no = (int)menswears[count].MenswearID;
                    page.PageNumber = page_number++;
                }
                page.PageData[line++] = string.Format("| {0, -2}  {1}", menswear_no, menswears[count].MenswearInfo);
                page.SaveIndex.Add(menswear_no++, (int)menswears[count++].MenswearID);
                if (count == count_menswears)
                {
                    pages.Add(page);
                    break;
                }
                if (line == 15)
                {
                    line = 0;
                    pages.Add(page);
                }
            }
            int current_page = 0;
            int y = 8;
            int topDown = 29;
            string[] menu = new string[] { "A. SEARCH MENSWEAR BY ID", "B. SEARCH MENSWEAR BY NAME", "ALL. SHOW LIST MENSWEAR", "ESC. EXIT" };
            do
            {
                style.ClearAt(18, 80 , 7, 25);
                Console.Clear();
                Frame("SELLER MENU");
                y = 10;
                topDown = 29;
                style.WriteAt("►   CHOICE MENU   ◄", 65, 28);
                for (int i = 0; i < menu.Length; i++)
                {
                    style.WriteAt(menu[i], 55, topDown++);
                }
                style.WriteAt(string.Format("----------------------------------------------------------------"), 18, 7);
                style.WriteAt(string.Format("| ID  | Menswear Name\t\t\t\t    | Price(VND) |"), 18, 8);
                style.WriteAt(string.Format("----------------------------------------------------------------"), 18, 9);
                for (int i = 0; i < 15; i++)
                {
                    style.WriteAt("|     |                                           |            |", 18, y);
                    style.WriteAt(pages[current_page].PageData[i], 18, y++);
                }
                style.WriteAt(string.Format("----------------------------------------------------------------"), 18, y++);

                ChangePage(1, 26, current_page + 1, max_page);
                
                style.WriteAt("► Your Choice: ", 8, 29);
                choice = style.ReadString();
                switch (choice.ToUpper())
                {
                    case "ESCAPE":
                    case "A":
                    case "B":
                    case "ALL":
                        return choice;
                    case "LEFTARROW":
                        if (current_page != 0)
                        {
                            current_page--;
                        }
                        break;
                    case "RIGHTARROW":
                        if (current_page != max_page - 1)
                        {
                            current_page++;
                        }
                        break;
                    default:
                        int index, result;
                        int.TryParse(choice, out index);
                        if (IndexID.TryGetValue(index, out result))
                        {
                            return result.ToString();
                        }
                        else
                        {
                            Console.SetCursorPosition(8, 30);
                            style.TextColor("▲! Invalid! Please re-enter", ConsoleColor.Red);
                            Console.ReadKey();
                        }
                        break;
                }
            } while (choice != "Escape");
            return choice;
        }
        public void ShowDetails(Menswear menswear)
        {
            int x = 5;
            int y = 12;
            style.ClearAt(1, 90 , 7 , 27);
            style.WriteAt(string.Format("--------------------------------------------------------------------------------------------"), 4, 8);
            style.WriteAt(string.Format("|\t\t\t\t\t      Details Menswear\t\t\t\t        | "), 4, 9);
            style.WriteAt(string.Format("--------------------------------------------------------------------------------------------"), 4, 10);
            style.WriteAt($"► Menswear ID: {menswear.MenswearID} ", x, y++);
            style.WriteAt($"► Menswear Name: {menswear.MenswearName} ", x, y++);
            style.WriteAt($"► Description: {menswear.Description}", x, y++);
            style.WriteAt($"► Brand: {menswear.Brand}", x, y++);
            style.WriteAt($"► Material: {menswear.Material}", x, y++);
            style.WriteAt($"► Price: {menswear.Price} VND", x, y++);
            style.WriteAt($"► Category: {menswear.MenswearCategory.CategoryName}", x, y++);
            style.WriteAt($"► Color: {menswear.ColorSizeList.ColorID.ColorName}", x, y++);
            style.WriteAt($"► Size: {menswear.ColorSizeList.SizeID.SizeName}", x, y++);
            style.WriteAt($"► Quantity: {menswear.ColorSizeList.Quantity}", x, y++);
            style.WriteAt(string.Format("--------------------------------------------------------------------------------------------"), 4, 23);
        }
        public static void ChangePage(int x, int y, int current_page, int max_page)
        {
            string back = "←";
            string next = "→";
            if (current_page == 1)
            {
                style.WriteAt(next, x + 54, y);
            }
            else if (current_page == max_page)
            {
                style.WriteAt(back, x + 44, y);
            }
            else
            {
                style.WriteAt(back, x + 44, y);
                style.WriteAt(next, x + 54, y);
            }
            style.WriteAt(string.Format(current_page + "/" + max_page), 50 - string.Format(current_page + "/" + max_page).Length / 2, y);
        }

        static void Frame(string tilte)
        {
            
            Console.WriteLine("┌──────────────────────────────────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                               ╔╦╗┌─┐┌┐┌┌─┐┬ ┬┌─┐┌─┐┬─┐  ╔═╗┌┬┐┌─┐┬─┐┌─┐                          │");
            Console.WriteLine("│                        ♦      ║║║├┤ │││└─┐│││├┤ ├─┤├┬┘  ╚═╗ │ │ │├┬┘├┤      ♦                    │");
            Console.WriteLine("│                               ╩ ╩└─┘┘└┘└─┘└┴┘└─┘┴ ┴┴└─  ╚═╝ ┴ └─┘┴└─└─┘                          │");
            Console.WriteLine("╞──────────────────────────────────────────────────────────────────────────────────────────────────╡");
            Console.WriteLine("│                                        ♦     {0, -10}     ♦                                   │", tilte);
            Console.WriteLine("╞──────────────────────────────────────────────────────────────────────────────────────────────────╡");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("╞──────────────────────────────────────────────────────────────────────────────────────────────────╡");
            Console.WriteLine("│                                                 │                                                │");
            Console.WriteLine("│                                                 │                                                │");
            Console.WriteLine("│                                                 │                                                │");
            Console.WriteLine("│                                                 │                                                │");
            Console.WriteLine("│                                                 │                                                │");
            Console.WriteLine("└──────────────────────────────────────────────────────────────────────────────────────────────────┘");
        }
    }
}