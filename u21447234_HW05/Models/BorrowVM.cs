using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21447234_HW05.ViewModels
{
    public class BorrowVM
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int BookID { get; set; }
        public string BORROWED_BY { get; set; }
        public DateTime TAKEN_DATE { get; set; }
        public DateTime BROUGHT_DATE { get; set; }
    }
}