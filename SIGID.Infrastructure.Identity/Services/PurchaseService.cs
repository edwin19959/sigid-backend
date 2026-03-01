using Microsoft.EntityFrameworkCore;
using SIGID.Core.Application.DTO.Purchases;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Domain.Entities;

namespace SIGID.Infrastructure.Persistence.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ApplicationDbContext _context;

        public PurchaseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PurchaseDTO>> GetAllAsync()
        {
            return await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Details).ThenInclude(d => d.Product)
                .Select(p => new PurchaseDTO
                {
                    Id = p.Id,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier.Name,
                    Date = p.Date,
                    Total = p.Total,
                    Status = p.Status,
                    Details = p.Details.Select(d => new PurchaseDetailDTO
                    {
                        ProductId = d.ProductId,
                        ProductName = d.Product.Name,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<PurchaseDTO?> GetByIdAsync(string id)
        {
            var p = await _context.Purchases
                .Include(p => p.Supplier)
                .Include(p => p.Details).ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (p == null) return null;
            return new PurchaseDTO
            {
                Id = p.Id,
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier.Name,
                Date = p.Date,
                Total = p.Total,
                Status = p.Status,
                Details = p.Details.Select(d => new PurchaseDetailDTO
                {
                    ProductId = d.ProductId,
                    ProductName = d.Product.Name,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };
        }

        public async Task<PurchaseDTO> CreateAsync(CreatePurchaseDTO dto)
        {
            var purchase = new Purchase
            {
                Id = Guid.NewGuid().ToString(),
                SupplierId = dto.SupplierId,
                Date = DateTime.UtcNow,
                Status = dto.Status ?? "Activo",
                Details = dto.Details.Select(d => new PurchaseDetail
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };
            purchase.Total = purchase.Details.Sum(d => d.Quantity * d.UnitPrice);

            // Actualizar stock de productos
            foreach (var detail in purchase.Details)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                if (product != null) product.CurrStock += detail.Quantity;
            }

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(purchase.Id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null) return false;
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
