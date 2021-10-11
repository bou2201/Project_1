using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class MenswearDALTest
    {
        private MenswearDAL menswearDAL = new MenswearDAL();
        private Menswear menswear = new Menswear();

        [Theory]
        [InlineData(1)]
        [InlineData(5)]        
        [InlineData(12)]        
        [InlineData(25)]        
        [InlineData(50)]        
        public void SearchByID_TestTrue(int id)
        {
            menswear.MenswearID = id;
            var result = menswearDAL.SearchByID(id);
            Assert.True(result != null);      
            Assert.True(result.MenswearID == id);     
        }

        [Theory]
        [InlineData(0)]
        [InlineData(75)]
        [InlineData(513)]
        [InlineData(100)]
        [InlineData(99)]
        public void SearchByID_TestFalse(int id)
        {
            menswear.MenswearID = id;
            var result = menswearDAL.SearchByID(id);
            Assert.True(result == null);          
        }

        [Theory]
        [InlineData("Polo Venus")]
        [InlineData("Sport Shirt Shazam")]
        [InlineData("T-Shirt ABS")]
        [InlineData("Tanktop 2019")] 
        [InlineData("Jean Balance")]        
        public void SearchByName_TestTrue(string name)
        {
            menswear.MenswearName = name;
            var result = menswearDAL.SearchByName(1, menswear);
            Assert.Contains(result, menswear => menswear.MenswearName == name);
        } 

        [Theory]
        [InlineData("Quan que")]
        [InlineData("Ao ba lo")]
        [InlineData("Ao rach")]
        [InlineData("Ao tang hinh")]
        [InlineData("Quan rach")] 
        public void SearchByName_TestFalse(string name)
        {
            menswear.MenswearName = name;
            var result = menswearDAL.SearchByName(1, menswear);
            Assert.DoesNotContain(result, menswear => menswear.MenswearName == name);
        }   

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetPriceByID_Test(int id)
        {            
            var result = menswearDAL.GetPriceByID(id);
            Assert.True(result != null);     
        }
    }
}