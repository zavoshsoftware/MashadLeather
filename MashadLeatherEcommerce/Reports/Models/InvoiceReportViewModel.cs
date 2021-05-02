using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reports.Models
{
    public class InvoiceReportViewModel
    {
        public string Address { get; set; }

        public string OrderCode { get; set; }
    
        public string CellNumber{ get; set; }
    
        public string FullName { get; set; }
      
        public string Total { get; set; }
     
        public string PostalCode { get; set; }
        public string City { get; set; }
     
    }

}