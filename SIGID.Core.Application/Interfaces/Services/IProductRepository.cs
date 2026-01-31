using SIGID.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
    }
}
