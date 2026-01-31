using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Infrastructure.Persistence.Repositories;

namespace SIGID.Infrastructure.Persistence
{
    public static class ServicesRegister
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
            // Registrar repositorio
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
