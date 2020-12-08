using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RestauranteMexicano.Startup))]
namespace RestauranteMexicano
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
