using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F2021A5HBL.Startup))]
namespace F2021A5HBL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
