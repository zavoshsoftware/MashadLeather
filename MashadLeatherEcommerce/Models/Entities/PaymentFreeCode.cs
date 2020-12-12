using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class PaymentFreeCode:BaseEntity
    {
        public Int64 Code { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}