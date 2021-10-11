using Xunit;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace DALTest
{
    public class InvoiceDALTest
    {
        private InvoiceDAL invoiceDAL = new InvoiceDAL();
        private Invoice invoice = new Invoice();
        
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
                    new Menswear { MenswearID = 1, ColorSizeList = new MenswearTable() {Quantity = 1}},
                    new Menswear { MenswearID = 12, ColorSizeList = new MenswearTable() {Quantity = 1}},
                    new Menswear { MenswearID = 23, ColorSizeList = new MenswearTable() {Quantity = 1}},
                    new Menswear { MenswearID = 45, ColorSizeList = new MenswearTable() {Quantity = 1}}
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
                CustomerInfo = new Customer { CustomerName = "Tran Thi Ngu", PhoneNumber = "0126425813"},
                SellerInfo = new Seller { SellerID = 1},
                ListMenswear =
                {
                    new Menswear { MenswearID = 10, ColorSizeList = new MenswearTable() {Quantity = 1}},
                    new Menswear { MenswearID = 20, ColorSizeList = new MenswearTable() {Quantity = 1}},
                    new Menswear { MenswearID = 30, ColorSizeList = new MenswearTable() {Quantity = 1}},
                    new Menswear { MenswearID = 40, ColorSizeList = new MenswearTable() {Quantity = 1}}
                }
            };
            var result = invoiceDAL.CreateInvoice(invoice);
            Assert.True(result);
        }

        [Fact]
        public void CreateInvoice_Test4()
        {
            Invoice invoice = new Invoice()
            {
                CustomerInfo = new Customer { CustomerName = "Nguyen C", PhoneNumber = "0326486936"},
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
        
        [Theory]
        [InlineData(1, "0328482434")]
        [InlineData(2, "0123456789")]
        [InlineData(3, "0321654987")]
        // [InlineData(4, "064251226")]
        // [InlineData(5, "0223651936")]
        public void GetHistory_TestTrue(int invoice_no, string phoneCustomer)
        {
            invoice.InvoiceNo = invoice_no;
            invoice.CustomerInfo.PhoneNumber = phoneCustomer;
            var result = invoiceDAL.GetListInvoice(invoice);
            Assert.Contains(result, invoice => invoice.InvoiceNo == invoice_no);
            Assert.Contains(result, invoice => invoice.CustomerInfo.PhoneNumber == phoneCustomer);
        }

        [Theory]
        [InlineData(100, "a5w4g64")]
        [InlineData(200, "126216")]
        [InlineData(324, "032165413613987")]
        [InlineData(4654, "")]
        [InlineData(521, "0513787")]
        public void GetHistory_TestFalse(int invoice_no, string phoneCustomer)
        {
            invoice.InvoiceNo = invoice_no;
            invoice.CustomerInfo.PhoneNumber = phoneCustomer;
            var result = invoiceDAL.GetListInvoice(invoice);
            Assert.DoesNotContain(result, invoice => invoice.InvoiceNo == invoice_no);
            Assert.DoesNotContain(result, invoice => invoice.CustomerInfo.PhoneNumber == phoneCustomer);
        }
    }
}