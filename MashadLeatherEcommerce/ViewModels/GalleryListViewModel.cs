using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class GalleryListViewModel:_BaseViewModel
    {
        public List<SiteGallery> Galleries { get; set; }
        public SiteGalleryGroup GalleryGroup { get; set; }
    }
}