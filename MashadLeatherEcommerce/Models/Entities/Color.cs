﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Helper;

namespace Models
{
    public class Color:BaseEntity
    {
        public Color()
        {
            Products = new List<Product>();
        }
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Color))]
        public string Title { get; set; }
        [Display(Name = "TitleEn", ResourceType = typeof(Resources.Models.Color))]
        public string TitleEn { get; set; }

        [Display(Name = "عنوان عربی")]
        public string TitleAr { get; set; }
        public string HexCode { get; set; }
        public string BarCodeId { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        GetCulture oGetCulture = new GetCulture();

        [NotMapped]
        public string TitleSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.TitleEn;
                    case "fa-ir":
                        return this.Title;
                    case "ar-ae":
                        return this.TitleAr;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}