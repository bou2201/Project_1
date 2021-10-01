using System;
using System.Collections.Generic;

namespace Persistence
{   
    public class Menswear
    {
        public int? MenswearID {set; get;}
        public string MenswearName {set; get;}
        public Category MenswearCategory {set; get;}
        public decimal Price {set; get;}
        public int? Amount {set; get;}
        public string Material {set; get;}
        public string Description {set; get;}
        public string Brand {set; get;}
        // public MenswearTable[] ColorSizeList {set; get;}
        public MenswearTable ColorSizeList {set; get;}

        public string MenswearInfo
        {
            get {return string.Format("| {0, -41} |    {1, 2} |", MenswearName, Price);}
        }
        public Menswear()
        {
            ColorSizeList = new MenswearTable();
            MenswearCategory = new Category();
        }                                         
    }
}