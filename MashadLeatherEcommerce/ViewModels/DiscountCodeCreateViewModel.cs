using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class DiscountCodeCreateViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(10, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Code { get; set; }

        [Display(Name = "تاریخ انقضا")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public DateTime ExpireDate { get; set; }

        [Display(Name = "حداکثر مقدار تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal MaxAmount { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public decimal Amount { get; set; }

        [Display(Name = "چند بار مصرف")]
        public bool IsMultiUsing { get; set; }

        [Display(Name = "شماره موبایل کاربر")]
        public string UserCellNumber { get; set; }

        [Display(Name = "کد عمومی است؟")]
        public bool IsPublic { get; set; }

 
        [Display(Name = "Description", ResourceType = typeof(Resources.Models.BaseEntity))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Resources.Models.BaseEntity))]
        public bool IsActive { get; set; }

        [Display(Name = "استفاده شده است؟")]
        public bool IsUsed { get; set; }
    }
}