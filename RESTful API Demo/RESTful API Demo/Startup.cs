using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("AntAlbelliTechnical.Startup", typeof(AntAlbelliTechnical.Startup))]

namespace AntAlbelliTechnical
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}