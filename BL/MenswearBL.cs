using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class MenswearBL
    {
        private MenswearDAL menswearDAL = new MenswearDAL();
        
        public Menswear SearchByID(int menswearId)
        {
            return menswearDAL.SearchByID(menswearId);
        }

        public List<Menswear> GetAll()
        {
            return menswearDAL.SearchByName(MenswearFilter.GET_ALL, null);
        }

        public List<Menswear> GetPriceByID(int invoiceId)
        {
            return menswearDAL.GetPriceByID(invoiceId);
        }

        public List<Menswear> SearchByName(string name)
        {
            return menswearDAL.SearchByName(MenswearFilter.FILTER_BY_NAME, new Menswear{MenswearName = name});
        }
    }
}