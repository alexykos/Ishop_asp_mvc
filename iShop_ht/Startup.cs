using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iShop_ht.Startup))]
namespace iShop_ht
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
