using Microsoft.EntityFrameworkCore;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGID.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
