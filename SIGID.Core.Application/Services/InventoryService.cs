using AutoMapper;
using SIGID.Core.Application.DTO.Inventory;
using SIGID.Core.Application.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public InventoryService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<InventoryStatsDTO> GetInventoryStatsAsync()
        {
            var stats = new InventoryStatsDTO
            {
                TotalProducts = await _productRepository.GetTotalProductsAsync(),
                TotalInventoryValue = await _productRepository.GetTotalStockValueAsync(),
                LowStockProducts = await _productRepository.GetLowStockCountAsync()
            };


            return stats;
        }

        public async Task<List<LowStockProductDTO>> GetLowStockProductsAsync()
        {
            var productos = await _productRepository.GetProductsWithLowStockAsync();
            return _mapper.Map<List<LowStockProductDTO>>(productos);
        }
    }
}