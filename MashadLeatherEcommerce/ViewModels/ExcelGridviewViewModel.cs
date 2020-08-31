using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ExcelGridviewViewModel
    {
        [Display(Name = "Code", ResourceType = typeof(Resources.Models.Order))]
        public int Code { get; set; }

        [Display(Name = "SaleReferenceId", ResourceType = typeof(Resources.Models.Order))]
        public string SaleReferenceId { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Resources.Models.OrderStatus))]
        public string OrderStatus { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Models.User))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Models.User))]
        public string LastName { get; set; }

        [Display(Name = "CellNum", ResourceType = typeof(Resources.Models.User))]
        public string CellNum { get; set; }

        [Display(Name = "CityTitle", ResourceType = typeof(Resources.Models.City))]
        public string CityTitle { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resources.Models.Order))]
        public string Address { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Order))]
        public string TotalAmount { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime CreationDate { get; set; }






    }
    public class ExcelGridviewForImageViewModel
    {
        public string ProductCode { get; set; }
    }



    public class ExcelGridviewUserReportViewModel
    {
        [Display(Name = "Code", ResourceType = typeof(Resources.Models.Order))]
        public int Code { get; set; }

        [Display(Name = "FirstName", ResourceType = typeof(Resources.Models.User))]
        public string FirstName { get; set; }

        [Display(Name = "LastName", ResourceType = typeof(Resources.Models.User))]
        public string LastName { get; set; }

        [Display(Name = "CellNum", ResourceType = typeof(Resources.Models.User))]
        public string CellNum { get; set; }
        
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Models.BaseEntity))]
        public System.DateTime CreationDate { get; set; }


    }

    public class ExcelGridviewCommentsReportViewModel
    {
       
        public string FullName { get; set; }

        public string Email { get; set; }

        public string ProductGroup { get; set; }
        public string Comment { get; set; }
        public string Response { get; set; }

        public System.DateTime CreationDate { get; set; }


    }
}