using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class DiscountCode:BaseEntity
    {
        public DiscountCode()
        {
            Orders=new List<Order>();
        }
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(10, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Code { get; set; }

        [Display(Name = "تاریخ انقضا")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public DateTime ExpireDate { get; set; }

        [Display(Name = "درصدی؟")]
        public bool IsPercent { get; set; }

        [Display(Name = "مقدار تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")] 
        public decimal Amount { get; set; }

        [Display(Name = "چند بار مصرف")]
        public bool IsMultiUsing { get; set; }

        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        internal class Configuration : EntityTypeConfiguration<DiscountCode>
        {
            public Configuration()
            {
                HasOptional(p => p.User)
                    .WithMany(j => j.DiscountCodes)
                    .HasForeignKey(p => p.UserId);

             
            }
        }
    }
}
