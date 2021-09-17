using System;
using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class SellerDALTest
    {
        private SellerDAL dal = new SellerDAL();
        private Seller seller = new Seller();

        [Fact]
        public void LoginTest1()
        {
            seller.Username = "admin1";
            seller.Password = "Menswear22@";
            var result = dal.Login(seller);
            Assert.True(result);
        }

        [Theory]
        [InlineData("admin", "Menswear22@")]
        [InlineData("admin2", "menswear@")]
        [InlineData("", "Menswear22@")]
        [InlineData("", "")]
        public void LoginTest2(string Username, string Password)
        {
            seller.Username = Username;
            seller.Password = Password;
            var result = dal.Login(seller);
            Assert.False(result);
        }
    }
}
