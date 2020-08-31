using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class TextType:BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Text> Texts { get; set; }    
    }
}