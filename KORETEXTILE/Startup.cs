using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BEEERP.Startup))]
namespace BEEERP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
