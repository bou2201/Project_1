using Persistence;
using DAL;

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
    }
}