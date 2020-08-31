using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace ViewModels
{
    public class GalleryDetailViewModel : _BaseViewModel
    {
        public SiteGallery Gallery{ get; set; }
    }
}