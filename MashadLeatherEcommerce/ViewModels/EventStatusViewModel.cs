using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class EventStatusViewModel
    {
        public string Code { get; set; }
        public string Amount { get; set; }
        public List<EventStatusProduct> Products { get; set; }
    }

    public class EventStatusProduct
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public string Amount { get; set; }
    }
}