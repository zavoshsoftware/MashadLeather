using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class OrderDetailHistoryViewModel:_BaseViewModel
    {
       
        public List<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
        public string Amount { get; set; }
        public string ShippmentPrice { get; set; }
        public string Vat { get; set; }
        public string TotalPayment { get; set; }
    }
}