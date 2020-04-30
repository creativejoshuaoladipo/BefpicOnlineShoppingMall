using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BefpicOnlineshoppingMall.Startup))]
namespace BefpicOnlineshoppingMall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
