using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class KiyanProductCategory : BaseEntity
    {
     
        public string Name { get; set; }
        public int PosDepartmentId { get; set; }

    }
}