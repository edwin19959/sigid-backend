using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SIGID.Infrastructure.Persistence
{
    public static class ServicesRegister
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Repositorios
            services.AddTransient<IProductRepository, ProductRepository>();

            // Servicios
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISupplierService, SupplierService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<ISaleService, SaleService>();
        }
    }
}
