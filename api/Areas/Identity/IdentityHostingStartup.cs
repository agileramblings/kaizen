using kaizen.api.Areas.Identity;
using kaizen.api.Areas.Identity.Data;
using kaizen.api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace kaizen.api.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<kaizenapiContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("kaizenapiContextConnection1")));

                services.AddDefaultIdentity<kaizenapiUser>()
                    .AddEntityFrameworkStores<kaizenapiContext>();
            });
        }
    }
}