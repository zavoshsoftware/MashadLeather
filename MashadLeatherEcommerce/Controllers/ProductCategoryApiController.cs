using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Models;
using ViewModels;

namespace MashadLeatherEcommerce.Controllers
{
    public class ProductCategoryApiController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();
        // GET api/<controller>
        [EnableCors(origins: "*", headers: "*", methods: "*")] // tune to your needs
        public List<ProductCategoryApiViewModel> Get()
        {
           // List<ProductCategory> productCategories = db.ProductCategories.ToList();
          //  return "productCategories";

            List<ProductCategoryApiViewModel> productCategories = new List<ProductCategoryApiViewModel>();

            List<ProductCategory> categories = db.ProductCategories.Where(current => current.IsActive == true&&current.IsDeleted==false).OrderBy(current=>current.Priority).ToList();

            foreach (ProductCategory productCategory in categories)
            {
                productCategories.Add(new ProductCategoryApiViewModel()
                {
                    Id = productCategory.Id,
                    Title = productCategory.Title,
                    ParentId = productCategory.ParentId,
                    HasChild = HasChild(productCategory.Id),
                    TitleEn = productCategory.TitleEn
                });
            }


            return productCategories;
        }

        public bool HasChild(Guid? parentId)
        {
            if (parentId != null)
            {
                if (db.ProductCategories.Any(current => current.ParentId == parentId))
                    return true;
                }
            return false;
        }



    }
}