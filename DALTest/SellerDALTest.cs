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
            seller.Password = "Menswear22@";
            var result = dal.Login(seller);
            Assert.True(result);
        }

        [Fact]
        public void LoginTest2()
        {
            seller.Username = "admin";
            seller.Password = "Menswear22@";
            var result = dal.Login(seller);
            Assert.False(result);
        }

        [Fact]
        public void LoginTest3()
        {
            seller.Username = "admin2002";
            seller.Password = "menswear@";
            var result = dal.Login(seller);
            Assert.False(result);
        }

        [Fact]
        public void LoginTest4()
        {
            seller.Username = "";
            seller.Password = "Menswear22@";
            var result = dal.Login(seller);
            Assert.False(result);
        }

        [Fact]
        public void LoginTest5()
        {
            seller.Username = "";
            seller.Password = "";
            var result = dal.Login(seller);
            Assert.False(result);
        }

        // wrong TEST
        // [Theory]
        // [InlineData("admin20023", "PF13VTCAcademy")]
        // [InlineData("admin20025", "PF13VTCAcademyis")]
        // public void LoginTest2(string Username, string Password)
        // {
        //     seller.Username = Username;
        //     seller.Password = Password;
        //     var result = dal.Login(seller);
        //     Assert.False(result);
        // }
    }
}
