using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(quanlysanxuat.StartupOwin))]

namespace quanlysanxuat
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
