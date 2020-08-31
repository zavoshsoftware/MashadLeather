using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class BlogDetailViewModel : _BaseViewModel
    {
        public Blog Blog { get; set; }
        public List<Blog> RelatedBlogs { get; set; }
    }
}