using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Helper
{
    
    public class GetCurrency
    {
        DatabaseContext db = new DatabaseContext();
        public string CurrentCurrency()
        {
            string currency = "none";
            if (HttpContext.Current.Request.Cookies["Currency"] != null)
            {
                currency = HttpContext.Current.Request.Cookies.Get("Currency").Value;
            }
            return currency;

        }

        public decimal CurrentEuroPrice()
        {
            return Convert.ToDecimal(db.Configurations.FirstOrDefault().Value);
        }
    }
}