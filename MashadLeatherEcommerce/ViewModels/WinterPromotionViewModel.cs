using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class WinterPromotionViewModel
    {
        [Required (ErrorMessage = "ایمیل خود را وارد نمایید.")]
        public string Email { get; set; }
        [Required (ErrorMessage = "شماره موبایل خود را وارد نمایید.")]
        public string CellNumber { get; set; }
    }
}