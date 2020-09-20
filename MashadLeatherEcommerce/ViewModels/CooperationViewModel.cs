using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class CooperationViewModel:_BaseViewModel
    {
        public string ResumeUrl { get; set; }
        public Text MainText { get; set; }
        public string HeaderImage { get; set; }
    }
}