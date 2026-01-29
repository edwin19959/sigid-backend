using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Infrastructure.Identity.Entities;
using SIGID.Infrastructure.Identity.Services;

namespace SIGID.Infrastructure.Identity
{
    public static class ServiceRegister
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //identity context
            services.AddDbContext<IdentityContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            //services
            services.AddScoped<IAccountService, AccountService>();


        }

    }
}
