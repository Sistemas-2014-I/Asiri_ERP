using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asiri_ERP_Capas.Startup))]
namespace Asiri_ERP_Capas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
