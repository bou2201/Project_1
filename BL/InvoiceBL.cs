using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class InvoiceBL
    {
        private InvoiceDAL invoiceDAL = new InvoiceDAL();

        public bool CreateInvoice(Invoice invoice)
        {
            bool result = invoiceDAL.CreateInvoice(invoice);
            return result;
        }
        
        public List<Invoice> GetListInvoice()
        {
            return invoiceDAL.GetListInvoice(null);
        }
    }
}