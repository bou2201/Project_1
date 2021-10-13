using System;
using Persistence;
using DAL;

namespace BL
{
    public class SellerBL
    {
        private SellerDAL sellerDAL = new SellerDAL();
        public Seller Login(Seller seller){
            return sellerDAL.Login(seller);
        }
    }
}
