using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class CareerType:BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Carreer> Carreers { get; set; }
    }
}