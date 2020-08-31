using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class KiyanProductItem
    {
        public decimal itmPrice { get; set; }

        public bool Taxable { get; set; }

        public string itmBrcd { get; set; }

        public string itmName { get; set; }

        public decimal itmQuantity { get; set; }

        public decimal itmTempPrice { get; set; }
        public int itmID { get; set; }
        public string itmCreateDate { get; set; }
    }
}