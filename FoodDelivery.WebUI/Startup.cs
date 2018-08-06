using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodDelivery.WebUI.Startup))]
namespace FoodDelivery.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
