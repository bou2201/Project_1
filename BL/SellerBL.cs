using System;
using Persistence;
using DAL;

namespace BL
{
    public class SellerBL
    {
        private SellerDAL dal = new SellerDAL();
        public int Login(Seller seller){
            return dal.Login(seller);
        }
    }
}
