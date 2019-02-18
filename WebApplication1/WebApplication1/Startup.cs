using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VetsEvents.Startup))]
namespace VetsEvents
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
