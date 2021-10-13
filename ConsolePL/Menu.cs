using System;
using System.Collections.Generic;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    public class Menu
    {
        private Style style = new Style();
        private List<Menswear> menswears;
        private List<Invoice> invoices;
        private MenswearBL menswearBL = new MenswearBL();
        private InvoiceBL invoiceBL = new InvoiceBL();
        private CustomerBL customerBL = new CustomerBL();

        public void SellerMenu(Seller seller)
        {
            string choice = string.Empty;
            menswears = menswearBL.GetAll();
            choice = ShowSellerMenu(menswears);
            do
            {
                switch (choice.ToUpper())
                {
                    case "ESCAPE":
                        break;
                    case "A":
                        style.PrintPosition("► Input Menswear ID: ", 3, 32);
                        int id;
                        if (Int32.TryParse(Console.ReadLine(), out id))
                        {
                            Menswear mens = menswearBL.SearchByID(id);
                            if (mens != null)
                            {
                                ShowMenswearDetails(mens);
                            }
                            else
                            {
                                Console.SetCursorPosition(3, 33);
                                style.TextColor($"▲ ID: {id} doesn't exist !", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(3, 33);
                            style.TextColor("▲ Invalid ID !", ConsoleColor.Red);
                        }
                        Console.ReadLine();
                        choice = "ALL";
                        break;
                    case "B":
                        style.PrintPosition("► Input Name: ", 3, 32);
                        choice = style.ReadString();
                        menswears = menswearBL.SearchByName(choice);
                        choice = ShowSellerMenu(menswears);
                        break;
                    case "C":
                        Invoice invoice = new Invoice();
                        invoice.SellerInfo = seller;
                        CreatInvoice(invoice);
                        choice = "ALL";
                        break;
                    case "D":
                        invoices = invoiceBL.GetListInvoice();
                        ShowHistoryTransaction(invoices);
                        choice = "ALL";
                        break;
                    case "E":
                        style.PrintPosition("► Input Customer ID: ", 3, 32);
                        int customerId;
                        if (Int32.TryParse(Console.ReadLine(), out customerId))
                        {
                            Customer customer = customerBL.GetById(customerId);
                            if (customer != null)
                            {
                                ShowCustomerInfo(customer);
                            }
                            else
                            {
                                Console.SetCursorPosition(3, 33);
                                style.TextColor($"▲ ID: {customerId} doesn't exist !", ConsoleColor.Red);
                            }
                        }
                        else
                        {
                            Console.SetCursorPosition(3, 33);
                            style.TextColor("▲ Invalid ID !", ConsoleColor.Red);
                        }
                        Console.ReadLine();
                        choice = "ALL";
                        break;
                    case "ALL":
                        choice = ShowSellerMenu(menswearBL.GetAll());
                        break;
                    default:
                        int idMens;
                        int.TryParse(choice, out idMens);
                        ShowMenswearDetails(menswearBL.SearchByID(idMens));
                        Console.ReadLine();
                        break;
                }
            } while (choice != "Escape");
        }

        public void ShowHistoryTransaction(List<Invoice> invoice)
        {
            if (invoice.Count == 0)
            {
                Console.SetCursorPosition(3, 32);
                style.TextColor("▲ No History !", ConsoleColor.Red);
                Console.ReadLine();
            }
            style.ClearPosition(1, 90, 7, 30);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 8);
            style.PrintPosition(string.Format("| No | Date Time               | Customer Name     | Customer Phone  |     Total Due (VND) |"), 4, 9);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 10);
            int y = 11;
            invoice.ForEach( x =>
            {
                x.ListMenswear = menswearBL.GetPriceByID(x.InvoiceNo);
                x.TotalDue = 0;
                x.ListMenswear.ForEach( pr =>
                {
                    x.TotalDue += (pr.Price * pr.Quantity);
                });
            });
            foreach (var s in invoice)
            {
                style.PrintPosition($"| {s.InvoiceNo,-3}| {s.InvoiceDate,-23} | {s.CustomerInfo.CustomerName,-17} | {s.CustomerInfo.PhoneNumber,-15} | {$"{s.TotalDue.ToString("N0")}", 19} |", 4, y++);
            }
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, y);
            Console.ReadKey();
        }

        public void CreatInvoice(Invoice invoice)
        {
            int id;
            int quantity;
            string str = string.Empty;
            style.ClearPosition(1, 90, 7, 30);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 8);
            style.PrintPosition(string.Format("|\t\t\t\t\t      Create Invoice\t\t\t\t        | "), 4, 9);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 10);
            do
            {
                style.ClearPosition(1, 90, 11, 30);
                style.PrintPosition("► Input Menswear ID: ", 8, 11);
                int.TryParse(Console.ReadLine(), out id);
                Menswear menswear = menswearBL.SearchByID(id);
                if (menswear == null)
                {
                    Console.SetCursorPosition(8, 12);
                    style.TextColor("▲ Invalid ID !", ConsoleColor.Red);
                }
                else
                {
                    Console.SetCursorPosition(8, 13);
                    Console.Write("► Input Quantity (Max is {0}): ", menswear.ColorSizeList.Quantity);
                    int.TryParse(Console.ReadLine(), out quantity);
                    if (quantity <= menswear.ColorSizeList.Quantity && quantity > 0)
                    {
                        menswear.ColorSizeList.Quantity = quantity;
                        if (menswear != null && menswear.ColorSizeList.Quantity > 0)
                        {
                            invoice.ListMenswear.Add(menswear);
                            Console.SetCursorPosition(8, 15);
                            style.TextColor("▲ Add To Invoice Successfully!", ConsoleColor.Green);
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(8, 14);
                        style.TextColor("▲ Invalid Quantity!", ConsoleColor.Red);
                    }

                }
                style.PrintPosition("Do You Want To Continue (Y/N): ", 8, 29);
                str = style.ReadString();
                switch (str.ToUpper())
                {
                    case "Y":
                    case "N":
                        break;
                    default:
                        Console.SetCursorPosition(8, 29);
                        style.TextColor("▲! Invalid! Please Re-enter To Continue", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
            } while (str.ToUpper() != "N");
            if (invoice.ListMenswear.Count == 0)
            {
                return;
            }
            else
            {
                style.ClearPosition(1, 90, 11, 30);
                style.PrintPosition(" ► Customer Information", 8, 12);
                style.PrintPosition(" ► Phone: ", 8, 14);
                invoice.CustomerInfo.PhoneNumber = Console.ReadLine();
                style.PrintPosition(" ► Name: ", 8, 16);
                invoice.CustomerInfo.CustomerName = Console.ReadLine();
                decimal totalDue = 0;
                foreach (var m in invoice.ListMenswear)
                {
                    totalDue += m.Price * m.ColorSizeList.Quantity;
                }
                style.PrintPosition($" - TOTAL DUE: {totalDue:N0} VND", 8, 18);
                decimal money = 0;
                string pay;
                do
                {
                    style.ClearPosition(1, 90, 20, 30);
                    style.PrintPosition("Do You Want To Payment(Y/N): ", 8, 20);
                    pay = style.ReadString();
                    switch (pay.ToUpper())
                    {
                        case "N":
                            return;
                        case "Y":
                            break;
                        default:
                            Console.SetCursorPosition(8, 22);
                            style.TextColor("▲ Invalid Choice", ConsoleColor.Red);
                            Console.ReadKey();
                            break;
                    }
                } while (pay.ToUpper() != "Y");

                do
                {
                    style.ClearPosition(1, 90, 22, 30);
                    style.PrintPosition(" - Enter Money: ", 8, 22);
                    decimal.TryParse(Console.ReadLine(), out money);
                    if (money < totalDue)
                    {
                        Console.SetCursorPosition(8, 24);
                        style.TextColor("▲ Invalid Money !", ConsoleColor.Red);
                        Console.ReadKey();
                    }
                } while (money < totalDue);
                decimal change = money - totalDue;
                Console.SetCursorPosition(8, 24);
                style.TextColor("▲ Press Any Key To Export ...", ConsoleColor.Green);
                Console.ReadLine();
                ExportInvoice(invoice, totalDue, change, money);
            }
        }
        public void ExportInvoice(Invoice invoice, decimal totalDue, decimal change, decimal money)
        {
            decimal amount = 0;
            int totalQuantity = 0;
            Console.Clear();
            Console.WriteLine("┌────────────────────────────────────────────────────────────────────────────┐");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine("│             ██ ███    ██ ██    ██  ██████  ██  ██████ ███████       ♥      │");
            Console.WriteLine("│             ██ ████   ██ ██    ██ ██    ██ ██ ██      ██                   │");
            Console.WriteLine("│             ██ ██ ██  ██ ██    ██ ██    ██ ██ ██      █████                │");
            Console.WriteLine("│             ██ ██  ██ ██  ██  ██  ██    ██ ██ ██      ██                   │");
            Console.WriteLine("│             ██ ██   ████   ████    ██████  ██  ██████ ███████              │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine($"│   Seller         : {invoice.SellerInfo.SellerName,-20}                                    │");
            Console.WriteLine($"│   Date           : {invoice.InvoiceDate,-30}                          │");
            Console.WriteLine($"│   Customer Name  : {invoice.CustomerInfo.CustomerName,-30}                          │");
            Console.WriteLine($"│   Customer Phone : {invoice.CustomerInfo.PhoneNumber,-30}                          │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine("│    Product                      Quantity           Price          Amount   │");
            Console.WriteLine("│ ────────────────────────────────────────────────────────────────────────── │");
            foreach (var m in invoice.ListMenswear)
            {
                totalQuantity += m.ColorSizeList.Quantity;
                amount = m.Price * m.ColorSizeList.Quantity;
                Console.WriteLine("│  {0, -25}    {1, 10}         {2, 4}      {3, 10}   │",
                                m.MenswearName, m.ColorSizeList.Quantity, m.Price.ToString("N0"), amount.ToString("N0"));
            }
            Console.WriteLine("│ ────────────────────────────────────────────────────────────────────────── │");
            Console.WriteLine($"│   Total Due & Quantity :      {totalQuantity,10}             {totalDue.ToString("N0"), 15} VND   │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine($"│   Cash                 :                        {money.ToString("N0"), 20} VND   │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine($"│   Change               :                        {change.ToString("N0"), 20} VND   │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine("│               ──────────────────────────────────────────────               │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine("│                         ♥   THANKS FOR SHOPPING  ♥                         │");
            Console.WriteLine("│                                                                            │");
            Console.WriteLine("└────────────────────────────────────────────────────────────────────────────┘");
            try
            {
                bool result = invoiceBL.CreateInvoice(invoice);
                if (result)
                {
                    style.TextColor("\n\n\t\t       ▲ Export Invoice Successfully !", ConsoleColor.Green);
                }
                else
                {
                    style.TextColor("\n\n\t\t          ▲ Failed To Transaction !", ConsoleColor.Red);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
        public string ShowSellerMenu(List<Menswear> menswears)
        {
            if (menswears.Count == 0)
            {
                Console.SetCursorPosition(3, 33);
                style.TextColor("▲ Not Exist This Name !", ConsoleColor.Red);
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
            int topDown1 = 32;
            int topDown2 = 32;
            string[] menu1 = new string[] { "C. Create Invoice", "D. Transaction History", "E. Customer Information", "← →. Change Page" };
            string[] menu = new string[] { "A. Search By ID", "B. Search By Name", "ALL. List Menswear", "ESC. Exit" };
            do
            {
                style.ClearPosition(18, 80, 7, 28);
                Console.Clear();
                Frame("SELLER MENU");
                y = 12;
                topDown1 = 32;
                topDown2 = 32;
                style.PrintPosition("►   CHOICE MENU   ◄", 61, 31);
                for (int i = 0; i < menu.Length; i++)
                {
                    style.PrintPosition(menu[i], 47, topDown1++);
                }
                for (int i = 0; i < menu1.Length; i++)
                {
                    style.PrintPosition(menu1[i], 72, topDown2++);
                }
                style.PrintPosition(string.Format("----------------------------------------------------------------"), 18, 9);
                style.PrintPosition(string.Format("| ID  | Menswear Name\t\t\t\t    | Price(VND) |"), 18, 10);
                style.PrintPosition(string.Format("----------------------------------------------------------------"), 18, 11);
                for (int i = 0; i < 15; i++)
                {
                    style.PrintPosition("|     |                                           |            |", 18, y);
                    style.PrintPosition(pages[current_page].PageData[i], 18, y++);
                }
                style.PrintPosition(string.Format("----------------------------------------------------------------"), 18, y++);

                ChangePage(1, 29, current_page+1, max_page);

                style.PrintPosition("► Your Choice: ", 3, 31);
                choice = style.ReadString();
                switch (choice.ToUpper())
                {
                    case "ESCAPE":
                        break;
                    case "A":
                    case "B":
                    case "C":
                    case "D":
                    case "E":
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
                            Console.SetCursorPosition(3, 32);
                            style.TextColor("▲! Invalid! Please re-enter", ConsoleColor.Red);
                            Console.ReadKey();
                        }
                        break;
                }
            } while (choice != "Escape");
            return choice;
        }
        public void ShowMenswearDetails(Menswear menswear)
        {
            int x = 5;
            int y = 12;
            style.ClearPosition(1, 90, 7, 30);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 8);
            style.PrintPosition(string.Format("|\t\t\t\t\t      Details Menswear\t\t\t\t        | "), 4, 9);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 10);
            style.PrintPosition($"► Menswear ID: {menswear.MenswearID} ", x, y++);
            style.PrintPosition($"► Menswear Name: {menswear.MenswearName} ", x, y++);
            style.PrintPosition($"► Description: {menswear.Description}", x, y++);
            style.PrintPosition($"► Brand: {menswear.Brand}", x, y++);
            style.PrintPosition($"► Material: {menswear.Material}", x, y++);
            style.PrintPosition($"► Price: {menswear.Price.ToString("N0")} VND", x, y++);
            style.PrintPosition($"► Category: {menswear.MenswearCategory.CategoryName}", x, y++);
            style.PrintPosition($"► Color: {menswear.ColorSizeList.ColorID.ColorName}", x, y++);
            style.PrintPosition($"► Size: {menswear.ColorSizeList.SizeID.SizeName}", x, y++);
            style.PrintPosition($"► Quantity: {menswear.ColorSizeList.Quantity}", x, y++);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 23);
        }

        public void ShowCustomerInfo(Customer customer)
        {
            int x = 5;
            int y = 12;
            style.ClearPosition(1, 90, 7, 30);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 8);
            style.PrintPosition(string.Format("|\t\t\t\t\t   Customer Infomation\t\t\t\t        | "), 4, 9);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 10);
            style.PrintPosition($"► Customer ID: {customer.CustomerID}", x, y++);
            style.PrintPosition($"► Customer Name: {customer.CustomerName}", x, y++);
            style.PrintPosition($"► Customer Phone Number: {customer.PhoneNumber}", x, y++);
            style.PrintPosition(string.Format("--------------------------------------------------------------------------------------------"), 4, 16);
        }
        public void ChangePage(int x, int y, int current_page, int max_page)
        {
            string back = "←";
            string next = "→";
            if (current_page == 1)
            {
                style.PrintPosition(next, x + 54, y);
            }
            else if (current_page == max_page)
            {
                style.PrintPosition(back, x + 44, y);
            }
            else
            {
                style.PrintPosition(back, x + 44, y);
                style.PrintPosition(next, x + 54, y);
            }
            style.PrintPosition(string.Format(current_page + "/" + max_page), 50 - string.Format(current_page + "/" + max_page).Length / 2, y);
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
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("│                                                                                                  │");
            Console.WriteLine("╞────────────────────────────────────────┬─────────────────────────────────────────────────────────╡");
            Console.WriteLine("│                                        │                                                         │");
            Console.WriteLine("│                                        │                                                         │");
            Console.WriteLine("│                                        │                                                         │");
            Console.WriteLine("│                                        │                                                         │");
            Console.WriteLine("│                                        │                                                         │");
            Console.WriteLine("└────────────────────────────────────────┴─────────────────────────────────────────────────────────┘");
        }
    }
}