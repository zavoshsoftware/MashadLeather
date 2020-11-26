using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class BlackFridayViewModel : _BaseViewModel
    {
        public List<ProductCategory> ProductCategories { get; set; }
        public Text Text { get; set; }
    }
}