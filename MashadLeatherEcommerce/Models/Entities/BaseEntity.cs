using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Helper;

namespace Models
{
    public class BaseEntity : object
    {

        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public System.Guid Id { get; set; }
        [Display(Name = "IsActive", ResourceType = typeof(Resources.Models.BaseEntity))]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime CreationDate { get; set; }

        [Display(Name = "LastModifiedDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime? LastModifiedDate { get; set; }

        [Display(Name = "IsDeleted", ResourceType = typeof(Resources.Models.BaseEntity))]
        [System.ComponentModel.DefaultValue(false)]
        public bool IsDeleted { get; set; }

        [Display(Name = "DeletionDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime? DeletionDate { get; set; }
      
        [AllowHtml]
        [Display(Name = "Description", ResourceType = typeof(Resources.Models.BaseEntity))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "DescriptionEn", ResourceType = typeof(Resources.Models.BaseEntity))]
        [DataType(DataType.MultilineText)]
        public string DescriptionEn { get; set; }

        [AllowHtml]
        [Display(Name = "DescriptionAr", ResourceType = typeof(Resources.Models.BaseEntity))]
        [DataType(DataType.MultilineText)]
        public string DescriptionAr { get; set; }


        GetCulture oGetCulture = new GetCulture();

        [NotMapped]
        public string DescriptionSrt
        {
            get
            {
                string currentCulture = oGetCulture.CurrentLang();
                switch (currentCulture.ToLower())
                {
                    case "en-us":
                        return this.DescriptionEn;
                    case "fa-ir":
                        return this.Description;
                    case "ar-ae":
                        return this.DescriptionAr;
                    default:
                        return String.Empty;
                }
            }
        }
    }
}
