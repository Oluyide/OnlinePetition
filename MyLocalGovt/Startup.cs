using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyLocalGovt.Startup))]
namespace MyLocalGovt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
