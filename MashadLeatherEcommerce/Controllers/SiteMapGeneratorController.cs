using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Helper;
using Models;

namespace MashadLeatherEcommerce.Controllers
{
    public class SiteMapGeneratorController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [Route("sitemap/fke2kb303ndkf")]
        public ActionResult Sitemap(string language)
        {
            Sitemap sm = new Sitemap();

            StaticPageSiteMap(sm);
            ProductGroupsSiteMap(sm);
            ProductsSiteMap(sm);
            BlogsSiteMap(sm);
            return new XmlResult(sm);
        }
 

        public void BlogsSiteMap(Sitemap sm)
        {
            AddToSiteMap(sm, "http://gassoco.com/fa/blog/", 0.7D, Location.eChangeFrequency.weekly);

            List<BlogGroup> blogGroups = db.BlogGroups.Where(c => c.IsDeleted == false).ToList();

            foreach (BlogGroup blogGroup in blogGroups)
            {
                AddToSiteMap(sm, "https://www.mashadleather.com/blog/"+ blogGroup.UrlParam, 0.9D, Location.eChangeFrequency.monthly);

            }

            List<Blog> blogs = db.Blogs.Where(current => current.IsDeleted == false).ToList();

            foreach (Blog blog in blogs)
            {
                AddToSiteMap(sm, "https://www.mashadleather.com/blog/"+blog.BlogGroup.UrlParam+"/"+blog.UrlParam , 0.9D, Location.eChangeFrequency.monthly);
            }
        }

        public void ProductsSiteMap(Sitemap sm)
        {
            List<Product> products = db.Products.Where(current => current.IsDeleted == false).ToList();

            foreach (Product product in products)
            {
                AddToSiteMap(sm, "https://www.mashadleather.com/product-detail/" + product.Code , 0.9D, Location.eChangeFrequency.monthly);
            }
        }

        public void ProductGroupsSiteMap(Sitemap sm)
        {
            List<ProductCategory> productGroups = db.ProductCategories
                .Where(current => current.IsDeleted == false).ToList();

            foreach (ProductCategory productGroup in productGroups)
            {
                if (db.Products.Any(current => current.ProductCategoryId==productGroup.Id && current.IsDeleted == false))
                {
                    AddToSiteMap(sm, "https://www.mashadleather.com/product/" + productGroup.UrlParam, 0.9D, Location.eChangeFrequency.weekly);
                }
                else
                {
                    AddToSiteMap(sm, "https://www.mashadleather.com/category/" + productGroup.UrlParam, 0.9D, Location.eChangeFrequency.weekly);

                }
            }

           
        }

        public void StaticPageSiteMap(Sitemap sm)
        {
            //Home
            AddToSiteMap(sm, "https://www.mashadleather.com/", 0.8D, Location.eChangeFrequency.weekly);
            AddToSiteMap(sm, "https://www.mashadleather.com/branches", 0.8D, Location.eChangeFrequency.weekly);
            AddToSiteMap(sm, "https://www.mashadleather.com/contact", 0.8D, Location.eChangeFrequency.weekly);
            AddToSiteMap(sm, "https://www.mashadleather.com/about", 0.8D, Location.eChangeFrequency.weekly);
            AddToSiteMap(sm, "https://www.mashadleather.com/customerclub", 0.8D, Location.eChangeFrequency.weekly);

        }

        public void AddToSiteMap(Sitemap sm, string url, double? priority, Location.eChangeFrequency frequency)
        {
            sm.Add(new Location()
            {
                // Url = string.Format("http://www.TechnoDesign.ir/Articles/{0}/{1}", 1, "SEO-in-ASP.NET-MVC"),
                Url = url,
                LastModified = DateTime.UtcNow,
            });
        }
    }
}