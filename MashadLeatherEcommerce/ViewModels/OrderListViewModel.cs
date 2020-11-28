using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class OrderListViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Code", ResourceType = typeof(Resources.Models.Order))]
        public int Code { get; set; }

        [Display(Name = "نحوه پرداخت")]
        public string PaymentType { get; set; }

        [Display(Name = "SaleReferenceId", ResourceType = typeof(Resources.Models.Order))]
        public Int64? SaleReferenceId { get; set; }

        [Display(Name = "OrderStatusId", ResourceType = typeof(Resources.Models.Order))]
        public string OrderStatusTitle { get; set; }
        public Guid OrderStatusId { get; set; }



        [Display(Name = "FirstName", ResourceType = typeof(Resources.Models.User))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Models.User))]
        public string LastName { get; set; }

        [Display(Name = "CellNum", ResourceType = typeof(Resources.Models.User))]
        public string CellNum { get; set; }

    
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Order))]
        public decimal TotalAmount { get; set; }


        [Display(Name = "Address", ResourceType = typeof(Resources.Models.Order))]
        public string Address { get; set; }

        [Display(Name = "شهر")]
        public string City { get; set; }

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


        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public string CreationDateStr
        {
            get
            {
                //  return "hi";
                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                string year = pc.GetYear(CreationDate).ToString().PadLeft(4, '0');
                string month = pc.GetMonth(CreationDate).ToString().PadLeft(2, '0');
                string day = pc.GetDayOfMonth(CreationDate).ToString().PadLeft(2, '0');
                return String.Format("{0}/{1}/{2}", year, month, day) + " " + CreationDate.ToString("HH:mm:ss");
            }
        }
    }
}