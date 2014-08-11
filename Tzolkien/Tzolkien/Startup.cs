using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tzolkien.Startup))]
namespace Tzolkien
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
