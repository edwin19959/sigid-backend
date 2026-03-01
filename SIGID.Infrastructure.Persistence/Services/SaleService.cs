using Microsoft.EntityFrameworkCore;
using SIGID.Core.Application.DTO.Sales;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Domain.Entities;

namespace SIGID.Infrastructure.Persistence.Services
{
    public class SaleService : ISaleService
    {
        private readonly ApplicationDbContext _context;

        public SaleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SaleDTO>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Details).ThenInclude(d => d.Product)
                .Select(s => new SaleDTO
                {
                    Id = s.Id,
                    ClientName = s.ClientName,
                    Date = s.Date,
                    Total = s.Total,
                    Status = s.Status,
                    Details = s.Details.Select(d => new SaleDetailDTO
                    {
                        ProductId = d.ProductId,
                        ProductName = d.Product.Name,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<SaleDTO?> GetByIdAsync(string id)
        {
            var s = await _context.Sales
                .Include(s => s.Details).ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (s == null) return null;
            return new SaleDTO
            {
                Id = s.Id, ClientName = s.ClientName, Date = s.Date,
                Total = s.Total, Status = s.Status,
                Details = s.Details.Select(d => new SaleDetailDTO
                {
                    ProductId = d.ProductId, ProductName = d.Product.Name,
                    Quantity = d.Quantity, UnitPrice = d.UnitPrice
                }).ToList()
            };
        }

        public async Task<SaleDTO> CreateAsync(CreateSaleDTO dto)
        {
            var sale = new Sale
            {
                Id = Guid.NewGuid().ToString(),
                ClientName = dto.ClientName,
                Date = DateTime.UtcNow,
                Status = dto.Status ?? "Activo",
                Details = dto.Details.Select(d => new SaleDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };
            sale.Total = sale.Details.Sum(d => d.Quantity * d.UnitPrice);

            // Reducir stock de productos
            foreach (var detail in sale.Details)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                if (product != null) product.CurrStock -= detail.Quantity;
            }

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(sale.Id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
