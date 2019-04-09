using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemWalter.Startup))]
namespace SistemWalter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
