using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ComputerWebShop.Startup))]
namespace ComputerWebShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
