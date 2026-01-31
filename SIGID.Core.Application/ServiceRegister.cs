using Microsoft.Extensions.DependencyInjection;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Application.Services;
using System.Reflection;

namespace SIGID.Core.Application
{
    public static class ServiceRegister
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            // Inject automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            // Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDashboardService, DashboardService>();
        }
    }
}
