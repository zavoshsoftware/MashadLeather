using Resources;

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Spatial;
    using System.Globalization;
    using System.Linq;

    public class User : BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Orders = new List<Order>();
            UserProductLike = new List<UserProductLike>();
            DiscountCodes = new List<DiscountCode>();
        }

        [Display(Name = "Username", ResourceType = typeof(Resources.Models.User))]
        [StringLength(150, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Username { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Resources.Models.User))]
        [StringLength(150, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "CellNum", ResourceType = typeof(Resources.Models.User))]
        [StringLength(30, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        [RegularExpression(@"(^(09|9)[0123456789][0123456789]\d{7}$)|(^(09|9)[0123456789][0123456789]\d{7}$)", ErrorMessageResourceName = "MobilExpersionValidation", ErrorMessageResourceType = typeof(Messages))]
        public string CellNum { get; set; }

    
        [Display(Name = "FirstName", ResourceType = typeof(Resources.Models.User))]
        [StringLength(250, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Models.User))]
        [StringLength(250, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string LastName { get; set; }
        [Display(Name = "Phone", ResourceType = typeof(Resources.Models.User))]
        [StringLength(50, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Phone { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Models.User))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Code { get; set; }

        [Display(Name = "CityTitle", ResourceType = typeof(Resources.Models.City))]
        public Guid? CityId { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resources.Models.User))]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "PostalCode", ResourceType = typeof(Resources.Models.User))]
        [StringLength(11, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string PostalCode { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.Models.User))]
        [StringLength(256, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessageResourceName = "EmailExpersionValidation", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; }

        public string Token { get; set; }

        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserProductLike> UserProductLike { get; set; }
        public virtual ICollection<DiscountCode> DiscountCodes { get; set; }



        internal class Configuration : EntityTypeConfiguration<User>
        {
            public Configuration()
            {
                HasOptional(p => p.City)
                    .WithMany(j => j.Users)
                    .HasForeignKey(p => p.CityId);

                HasRequired(p => p.Role)
                    .WithMany(j => j.Users)
                    .HasForeignKey(p => p.RoleId);
            }
        }

    }
}

