using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(customLms.Startup))]
namespace customLms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
