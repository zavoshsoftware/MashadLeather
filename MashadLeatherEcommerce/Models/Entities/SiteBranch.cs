using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Models
{
    public class SiteBranch : BaseEntity
    {
        [Display(Name="شعبه")]
        public string Title { get; set; }
        [Display(Name = "شعبه انگلیسی")]
        public string TitleEn { get; set; }

        [Display(Name = "شعبه عربی")]
        public string TitleAr { get; set; }

        [Display(Name="آدرس ")]
        public string Address { get; set; }
        [Display(Name = "آدرس انگلیسی")]
        public string AddressEn { get; set; }

        [Display(Name = "آدرس عربی")]
        public string AddressAr { get; set; }

        [Display(Name="تلفن")]
        public string Phone { get; set; }

        [Display(Name= "Latitude")]
        public string Latitude { get; set; }

        [Display(Name= "Longitude")]
        public string Longitude { get; set; }

        [Display(Name="شهر")]
        public Guid SiteBranchGroupId { get; set; }
        public virtual SiteBranchGroup SiteBranchGroup { get; set; }
        [Display(Name = "اولویت نمایش")]
        public int? Order { get; set; }
        internal class Configuration : EntityTypeConfiguration<SiteBranch>
        {
            public Configuration()
            {
                HasRequired(p => p.SiteBranchGroup)
                    .WithMany(j => j.SiteBranches)
                    .HasForeignKey(p => p.SiteBranchGroupId);
            }
        }

        [NotMapped]
        public string Location {
            get { return "http://maps.google.com?q=" + Latitude + "," + Longitude; } }

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
        [NotMapped]
        public string AddressSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.AddressEn;
                    case "fa-ir":
                        return this.Address;
                    case "ar-ae":
                        return this.AddressAr;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}