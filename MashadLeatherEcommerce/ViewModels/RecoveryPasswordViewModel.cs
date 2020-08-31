using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class RecoveryPasswordViewModel:_BaseViewModel
    {
        [Required(ErrorMessage = "شماره موبایلی که در وب سایت با آن ثبت نام کرده اید را وارد نمایید")]
        public string CellNumber { get; set; }
    }
}