using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Size : BaseEntity
    {
        public Size()
        {
            Products = new List<Product>();
        }
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Size))]
        public string Title { get; set; }
        public string BarCodeProductGroup { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}