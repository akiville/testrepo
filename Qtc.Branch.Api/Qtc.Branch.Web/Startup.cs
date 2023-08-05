using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Qtc.Branch.Web.Startup))]
namespace Qtc.Branch.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
