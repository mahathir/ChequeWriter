using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChequeWriter.Web.Startup))]
namespace ChequeWriter.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
