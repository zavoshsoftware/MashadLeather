using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class AboutViewModel : _BaseViewModel
    {
        public string HeaderImage { get; set; }
        public string MainText { get; set; }
        public List<Text> Numbers { get; set; }
        public List<Text> Certificates { get; set; }
        public List<Text> SaleSystem { get; set; }
        public string Partners { get; set; }
    }
}