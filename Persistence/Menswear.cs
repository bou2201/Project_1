using System;

namespace Persistence
{   
    public class Menswear
    {
        public int MenswearID {set; get;}
        public string MenswearName {set; get;}
        public Category MenswearCategory {set; get;}
        public double Price {set; get;}
        public string Material {set; get;}
        public string Description {set; get;}
        public string Brand {set; get;}
        public MenswearTable[] ColorSizeList {set; get;}
    }
}