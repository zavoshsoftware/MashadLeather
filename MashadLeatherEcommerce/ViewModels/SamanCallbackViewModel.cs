using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class SamanCallbackViewModel : _BaseViewModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string TrackNumber { get; set; }
        public string RefrenceNumber { get; set; }

        public string PaymentStatus
        {
            get
            {
                if (IsSuccess)
                    return "پرداخت موفق";
                else
                {
                    return "خطا در پرداخت";
                }
            }
        }
    }
}