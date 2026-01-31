using SIGID.Core.Application.DTO.Dashboard;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;

        public DashboardService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<DashboardStatsDTO> GetDashboardStatsAsync()
        {
            var productos = await _productRepository.GetAllAsync();
            
            var stats = new DashboardStatsDTO
            {
                TotalProductos = productos.Count,
                ValorTotalInventario = productos.Sum(p => p.CurrStock),
                ProductosStockBajo = productos.Count(p => p.CurrStock <= p.StockMin)
            };
            
            return stats;
        }

        public async Task<List<ProductStockBajoDTO>> GetProductosStockBajoAsync()
        {
            var productos = await _productRepository.GetAllAsync();
            
            var productosStockBajo = productos
                .Where(p => p.CurrStock <= p.StockMin)
                .OrderBy(p => p.CurrStock - p.StockMin)
                .Select(p => new ProductStockBajoDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CurrStock = p.CurrStock,
                    StockMin = p.StockMin,
                    DiferenciaStock = p.CurrStock - p.StockMin,
                    Status = p.Status
                })
                .ToList();
            
            return productosStockBajo;
        }
    }
}
