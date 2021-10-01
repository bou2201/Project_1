using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class CustomerDALTest
    {
        private CustomerDAL customerDAL = new CustomerDAL();
        private Customer customer = new Customer();

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetCustomerID_TestTrue(int customer_id)
        {
            customer.CustomerID = customer_id;
            var result = customerDAL.GetById(customer_id);
            Assert.True(result != null);
            Assert.True(result.CustomerID == customer_id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        [InlineData(156)]
        [InlineData(4483)]
        [InlineData(36)]
        [InlineData(15)]
        public void GetCustomerID_TestFalse(int customer_id)
        {
            customer.CustomerID = customer_id;
            var result = customerDAL.GetById(customer_id);
            Assert.False(result.CustomerID == customer_id);
            Assert.False(result.CustomerID != null);
        }
    }
}