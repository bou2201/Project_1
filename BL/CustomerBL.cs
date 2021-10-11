using Persistence;
using DAL;

namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL customerDAL = new CustomerDAL();
        public Customer GetById(int customerId)
        {
            return customerDAL.GetById(customerId);
        }
    }
}