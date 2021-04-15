using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Models;
using ViewModels;

namespace Helper
{
    public class ProductHelper
    {
        private readonly int _productPagination = Convert.ToInt32(WebConfigurationManager.AppSettings["productPaginationSize"]);
         
        public List<PageItem> GetPagination(int productCount, int? pageId)
        {
            List<PageItem> result = new List<PageItem>();

            int pageNumbers = (int)Math.Ceiling(productCount / (double)_productPagination);

            for (int i = 1; i <= pageNumbers; i++)
            {
                bool isActive = pageId == i;

                PageItem pageItem = new PageItem()
                {
                    PageId = i,
                    IsCurrentPage = isActive
                };
                result.Add(pageItem);
            }

            return result;
        }
     
    }
}