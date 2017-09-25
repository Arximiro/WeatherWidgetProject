using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeatherWidgetProject.Startup))]
namespace WeatherWidgetProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
