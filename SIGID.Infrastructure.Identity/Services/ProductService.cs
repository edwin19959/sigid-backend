using Microsoft.EntityFrameworkCore;
using SIGID.Core.Application.DTO.Products;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Domain.Entities;

namespace SIGID.Infrastructure.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            return await _context.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                StockMin = p.StockMin,
                CurrStock = p.CurrStock,
                Status = p.Status
            }).ToListAsync();
        }

        public async Task<ProductDTO?> GetByIdAsync(string id)
        {
            var p = await _context.Products.FindAsync(id);
            if (p == null) return null;
            return new ProductDTO { Id = p.Id, Name = p.Name, StockMin = p.StockMin, CurrStock = p.CurrStock, Status = p.Status };
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                StockMin = dto.StockMin,
                CurrStock = dto.CurrStock,
                Status = dto.Status
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductDTO { Id = product.Id, Name = product.Name, StockMin = product.StockMin, CurrStock = product.CurrStock, Status = product.Status };
        }

        public async Task<ProductDTO?> UpdateAsync(string id, UpdateProductDTO dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;
            product.Name = dto.Name;
            product.StockMin = dto.StockMin;
            product.CurrStock = dto.CurrStock;
            product.Status = dto.Status;
            await _context.SaveChangesAsync();
            return new ProductDTO { Id = product.Id, Name = product.Name, StockMin = product.StockMin, CurrStock = product.CurrStock, Status = product.Status };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
