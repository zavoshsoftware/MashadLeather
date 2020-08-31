using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MashadLeatherEcommerce.Startup))]
namespace MashadLeatherEcommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
