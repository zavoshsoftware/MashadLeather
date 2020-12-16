using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DiscountCode : BaseEntity
    {
        public DiscountCode()
        {
            Orders = new List<Order>();
        }

        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(50, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Code { get; set; }

        [Display(Name = "تاریخ انقضا")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public DateTime ExpireDate { get; set; }

        //[Display(Name = "درصدی؟")]
        //public bool IsPercent { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal Amount { get; set; }




        [Display(Name = "حداکثر مبلغ سفارش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal MaxAmount { get; set; }

        [Display(Name = "چند بار مصرف")]
        public bool IsMultiUsing { get; set; }

        [Display(Name = "کد عمومی است؟")]
        public bool IsPublic { get; set; }

        [Display(Name = "استفاده شده است؟")]
        public bool IsUsed { get; set; }

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

        [Display(Name = "اپراتور تغییر دهنده وضعیت")]
        public string OperatorUsername { get; set; }

        [Display(Name = "درصد تخفیف")]
        [NotMapped]
        public string AmountStr => Amount.ToString("N0");

        [Display(Name = "درصد تخفیف")]
        [NotMapped]
        public string AmountStr2 => (Amount + 15).ToString("N0");

        [Display(Name = "حداکثر مقدار تخفیف")]
        [NotMapped]
        public string MaxAmountStr => MaxAmount.ToString("N0");


        //[NotMapped]
        //[Display(Name = "درصد تخفیف")]
        //[Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        //public decimal? ShowOperatorAmount
        //{
        //    get
        //    {
        //        if (Amount<30)
        //            return Amount;
        //        else
        //            return Amount-15;
        //    }
        //}
    }
}
