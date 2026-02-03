using SIGID.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IProductRepository
    {
       
        Task<int> GetTotalProductsAsync();
        Task<int> GetTotalStockValueAsync();
        Task<int> GetLowStockCountAsync();
        Task<List<Product>> GetProductsWithLowStockAsync();

    }
}
