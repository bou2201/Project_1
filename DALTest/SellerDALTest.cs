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

        // right TEST
        [Fact]
        public void LoginTest1()
        {
            seller.Username = "admin2002";
            seller.Password = "PF13VTCAcademy";
            var result = dal.Login(seller);
            Assert.True(result);
        }

        // wrong TEST
        [Theory]
        [InlineData("admin20023", "PF13VTCAcademy")]
        [InlineData("admin20025", "PF13VTCAcademyis")]
        public void LoginTest2(string Username, string Password)
        {
            seller.Username = Username;
            seller.Password = Password;
            var result = dal.Login(seller);
            Assert.False(result);
        }
    }
}
