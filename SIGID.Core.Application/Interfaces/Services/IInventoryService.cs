using SIGID.Core.Application.DTO.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IInventoryService
    {
        Task<InventoryStatsDTO> GetInventoryStatsAsync();
        Task<List<LowStockProductDTO>> GetLowStockProductsAsync();
    }
}