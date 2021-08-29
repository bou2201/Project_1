using System;

namespace Persistence
{
    public class Invoice
    {
        public int InvoiceNo {set; get;}
        public double TotalDue {set; get;}
        public DateTime Orderdate {set; get;}
        public Customer CustomerInfo {set; get;}
        public Seller SellerInfo {set; get;}
        
    }
}