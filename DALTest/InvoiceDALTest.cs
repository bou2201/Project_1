using Xunit;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace DALTest
{
    public class InvoiceDALTest
    {
        private InvoiceDAL invoiceDAL = new InvoiceDAL();
        // private Invoice invoice = new Invoice();
        
        [Fact]
        public void CreateInvoice_Test1()
        {              
            Invoice invoice = new Invoice()
            {
                CustomerInfo = new Customer { CustomerName = "Nguyen A", PhoneNumber = "0326485136"},
                SellerInfo = new Seller { SellerID = 1},
                ListMenswear =
                {
                    new Menswear { MenswearID = 15, ColorSizeList = new MenswearTable() {Quantity = 2}},
                    new Menswear { MenswearID = 23, ColorSizeList = new MenswearTable() {Quantity = 3}},
                    new Menswear { MenswearID = 36, ColorSizeList = new MenswearTable() {Quantity = 4}},
                    new Menswear { MenswearID = 32, ColorSizeList = new MenswearTable() {Quantity = 1}}
                }
            };
            var result = invoiceDAL.CreateInvoice(invoice);
            Assert.True(result);
        }

        [Fact]
        public void CreateInvoice_Test2()
        {              
            Invoice invoice = new Invoice()
            {
                CustomerInfo = new Customer { CustomerName = "Nguyen B", PhoneNumber = "0854692136"},
                SellerInfo = new Seller { SellerID = 1},
                ListMenswear =
                {
                    new Menswear { MenswearID = 1, ColorSizeList = new MenswearTable() {Quantity = 5}},
                    new Menswear { MenswearID = 12, ColorSizeList = new MenswearTable() {Quantity = 6}},
                    new Menswear { MenswearID = 23, ColorSizeList = new MenswearTable() {Quantity = 6}},
                    new Menswear { MenswearID = 45, ColorSizeList = new MenswearTable() {Quantity = 2}}
                }
            };
            var result = invoiceDAL.CreateInvoice(invoice);
            Assert.True(result);
        }

        [Fact]
        public void CreateInvoice_Test3()
        {
            Invoice invoice = new Invoice()
            {
                CustomerInfo = new Customer { CustomerName = "Nguyen B", PhoneNumber = "0326486936"},
                SellerInfo = new Seller { SellerID = 1},
                ListMenswear =
                {
                    new Menswear { MenswearID = 0, ColorSizeList = new MenswearTable() {Quantity = 6}},
                    new Menswear { MenswearID = 100, ColorSizeList = new MenswearTable() {Quantity = 15}},
                    new Menswear { MenswearID = 3564, ColorSizeList = new MenswearTable() {Quantity = 26}},
                    new Menswear { MenswearID = 2, ColorSizeList = new MenswearTable() {Quantity = 600}}
                }
            };
            var result = invoiceDAL.CreateInvoice(invoice);
            Assert.False(result);
        }
    }
}