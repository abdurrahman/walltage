using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Walltage.WebUI.Startup))]
namespace Walltage.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}