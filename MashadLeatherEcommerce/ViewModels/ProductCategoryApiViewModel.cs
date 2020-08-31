using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ProductCategoryApiViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public Guid? ParentId { get; set; }
        public bool HasChild { get; set; }
    }
}