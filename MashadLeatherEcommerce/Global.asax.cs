using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using API;
using GSD.Globalization;

namespace MashadLeatherEcommerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            //GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-AE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-AE");

            //var persianCulture = new PersianCulture();
            //Thread.CurrentThread.CurrentCulture = persianCulture;
            //Thread.CurrentThread.CurrentUICulture = persianCulture;
        }
    }
}
