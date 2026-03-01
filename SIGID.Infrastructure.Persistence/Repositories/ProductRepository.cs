using Microsoft.EntityFrameworkCore;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Domain.Entities;

namespace SIGID.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalStockValueAsync()
        {
            var products = await _context.Products.ToListAsync();
            return (int)products.Sum(p => p.Price * p.CurrStock);
        }

        public async Task<int> GetLowStockCountAsync()
        {
            return await _context.Products
                .CountAsync(p => p.CurrStock <= p.StockMin);
        }

        public async Task<List<Product>> GetProductsWithLowStockAsync()
        {
            return await _context.Products
                .Where(p => p.CurrStock <= p.StockMin)
                .OrderBy(p => p.CurrStock - p.StockMin)
                .ToListAsync();
        }
    }
}
