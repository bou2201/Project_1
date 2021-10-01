using System;
using System.Collections.Generic;

namespace ConsoleAppPL
{
    public class Page
    {
        public int PageNumber{set; get;}
        public Dictionary<int, int> SaveIndex{set; get;}
        public string[] PageData{set; get;}
        public Page(){
            SaveIndex = new Dictionary<int, int>();
            PageData = new string[15];
        }
    }
}