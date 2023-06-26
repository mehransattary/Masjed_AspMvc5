using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Masjed.Startup))]
namespace Masjed
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
