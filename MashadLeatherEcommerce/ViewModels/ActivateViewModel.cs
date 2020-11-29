using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ActivateViewModel : _BaseViewModel
    {
        [Required(ErrorMessage = "کد فعالسازی")]
        public string ActivationCode { get; set; }
        public string CellNumber { get; set; }
    }
}