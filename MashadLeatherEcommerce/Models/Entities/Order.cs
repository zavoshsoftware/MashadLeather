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
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            Payments = new List<Payment>();
            PaymentUniqeCodes = new List<PaymentUniqeCodes>();
            PaymentFreeCodes = new List<PaymentFreeCode>();
        }

        [Display(Name = "Code", ResourceType = typeof(Resources.Models.Order))]
        [Required]
        public int Code { get; set; }

        [Display(Name = "UserId", ResourceType = typeof(Resources.Models.Order))]
        public Guid UserId { get; set; }


        [Display(Name = "Address", ResourceType = typeof(Resources.Models.Order))]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Order))]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "OrderStatusId", ResourceType = typeof(Resources.Models.Order))]
        [Required]
        public Guid OrderStatusId { get; set; }

        [Display(Name = "OrderFileUrl", ResourceType = typeof(Resources.Models.Order))]
        public string OrderFileUrl { get; set; }

        public Guid? CityId { get; set; }

        [Display(Name = "SaleReferenceId", ResourceType = typeof(Resources.Models.Order))]
        public Int64? SaleReferenceId { get; set; }

        [Display(Name = "BankName", ResourceType = typeof(Resources.Models.Order))]
        public string BankName { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PaymentUniqeCodes> PaymentUniqeCodes { get; set; }
        internal class Configuration : EntityTypeConfiguration<Order>
        {
            public Configuration()
            {
                HasRequired(p => p.User)
                    .WithMany(j => j.Orders)
                    .HasForeignKey(p => p.UserId);

                HasRequired(p => p.OrderStatus)
                    .WithMany(j => j.Orders)
                    .HasForeignKey(p => p.OrderStatusId);

                HasOptional(p => p.City)
                    .WithMany(j => j.Orders)
                    .HasForeignKey(p => p.CityId);

                HasOptional(p => p.DiscountCode)
                    .WithMany(j => j.Orders)
                    .HasForeignKey(p => p.DiscountCodeId);

              
 
            }
        }

        [Display(Name = "نحوه پرداخت")]
        public string PaymentType { get; set; }
        [NotMapped]
        [Display(Name = "نحوه پرداخت")]
        public string PaymentTypeTitle
        {
            get
            {
                if (PaymentType == "recieve")
                    return "پرداخت در محل";
                return "پرداخت آنلاین";
            }
        }

        public Guid? DiscountCodeId { get; set; }
        public virtual DiscountCode DiscountCode { get; set; }



        [Display(Name = "SubAmount", ResourceType = typeof(Resources.Models.Order))]
        [Column(TypeName = "Money")]
        public decimal? SubAmount { get; set; }



        [Display(Name = "ShipmentAmount", ResourceType = typeof(Resources.Models.Order))]
        [Column(TypeName = "Money")]
        public decimal? ShipmentAmount { get; set; }



        [Display(Name = "DiscountAmount", ResourceType = typeof(Resources.Models.Order))]
        [Column(TypeName = "Money")]
        public decimal? DiscountAmount { get; set; }


        [Display(Name = "مبلغ استفاده شده از کیف پول")]
        [Column(TypeName = "Money")]
        public decimal? WalletAmount { get; set; }
         
        [Display(Name = "مبلغ پرداختی")]
        [Column(TypeName = "Money")]
        public decimal? PaymentAmount { get; set; }


        [Display(Name = "انبار خروجی")]
        public string ExitInventory  { get; set; }
        [Display(Name = "PostalCode", ResourceType = typeof(Resources.Models.User))]
        public string PostalCode  { get; set; }
        public virtual ICollection<PaymentFreeCode> PaymentFreeCodes { get; set; }

        [Display(Name="فاکتور ارسال شود")]
        public bool SendFactor { get; set; }

    }
}
