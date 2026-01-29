using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SIGID.Infrastructure.Persistence
{
    public static class ServicesRegister
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
        {
            //inject 
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //repositories pending include
        }
    }
}
