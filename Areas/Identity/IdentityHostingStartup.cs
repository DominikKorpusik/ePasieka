using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ePasieka.Areas.Identity.IdentityHostingStartup))]
namespace ePasieka.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}