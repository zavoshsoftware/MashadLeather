 
namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Spatial;

    public class City : BaseEntity
    {
        public City()
        {
            Users = new List<User>();
            Orders = new List<Order>();
        }

        [Display(Name = "ProvinceTitle", ResourceType = typeof(Resources.Models.Province))]
        public Guid ProvinceId { get; set; }

        [StringLength(100)]
        [Display(Name = "CityTitle", ResourceType = typeof(Resources.Models.City))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Title { get; set; }

        [Display(Name = "IsCenterCity", ResourceType = typeof(Resources.Models.City))]
        public bool IsCenter { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        internal class Configuration : EntityTypeConfiguration<City>
        {
            public Configuration()
            {
                HasRequired(p => p.Province)
                    .WithMany(j => j.Cities)
                    .HasForeignKey(p => p.ProvinceId);
            }
        }
    }
}
