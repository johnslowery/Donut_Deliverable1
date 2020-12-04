using System;
using Donut_Deliverable1.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Donut_Deliverable1.Areas.Identity.IdentityHostingStartup))]
namespace Donut_Deliverable1.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AccountDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<IdentityUser>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AccountDbContext>();

            });
        }
    }
}