using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APITecsup.Models
{
    public class Detail
    {
        public int DetailID { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int SubTotal { get; set; }


        public int InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }



    }
}