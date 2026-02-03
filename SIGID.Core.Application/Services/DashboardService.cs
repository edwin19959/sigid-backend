
using AutoMapper;
using SIGID.Core.Application.DTO.Dashboard;
using SIGID.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DashboardService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<DashboardStatsDTO> GetDashboardStatsAsync()
        {
            var stats = new DashboardStatsDTO
            {
                TotalProductos = await _productRepository.GetTotalProductsAsync(),
                ValorTotalInventario = await _productRepository.GetTotalStockValueAsync(),
                ProductosStockBajo = await _productRepository.GetLowStockCountAsync()
            };

            return stats;
        }

        public async Task<List<ProductStockBajoDTO>> GetProductosStockBajoAsync()
        {
            var productos = await _productRepository.GetProductsWithLowStockAsync();
            return _mapper.Map<List<ProductStockBajoDTO>>(productos);
        }
    }
}