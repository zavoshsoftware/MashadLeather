using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class UserInformation:BaseEntity
    {
        public string Email { get; set; }
        public string CellNumber { get; set; }
    }
}