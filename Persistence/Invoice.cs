using System;
using System.Collections.Generic;

namespace Persistence
{
    public static class InvoiceStatus
    {
        public const int CREATE_NEW_INVOICE = 1;
        public const int INVOICE_INPROGRESS = 2;
    }
    public class Invoice
    {
        public int InvoiceNo {set; get;}
        public decimal TotalDue {set; get;}
        public DateTime InvoiceDate {set; get;}
        public Customer CustomerInfo {set; get;}
        public Seller SellerInfo {set; get;}
        public List<Menswear> ListMenswear {set; get;}
        
        public Menswear this[int index]
        {
            get
            {
                if (ListMenswear == null || ListMenswear.Count == 0 || index < 0 || ListMenswear.Count < index) return null;
                return ListMenswear[index];
            }
            set
            {
                if (ListMenswear == null) ListMenswear = new List<Menswear>();
                ListMenswear.Add(value);
            }
        }
        public Invoice()
        {
            ListMenswear = new List<Menswear>();
            CustomerInfo = new Customer();
            InvoiceDate = DateTime.Now;
        }
    }
}