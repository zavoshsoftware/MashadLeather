using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OldStoreRedirect
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

         

        }


        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            string url = HttpContext.Current.Request.Url.AbsoluteUri;

            string path = HttpContext.Current.Request.Url.AbsolutePath;

            // new line here
            HttpContext.Current.Response.Clear();
            //
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", Request.Url.ToString().ToLower().Replace(url, "https://www.mashadleather.com" + path));


        }
    }
}
