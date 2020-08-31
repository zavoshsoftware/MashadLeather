using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class RegisterViewModel:_BaseViewModel
    {
        public string CellNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CityId { get; set; }
        public string Password { get; set; }
    }
}