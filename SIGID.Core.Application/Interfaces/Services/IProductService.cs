using SIGID.Core.Application.DTO.Products;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByIdAsync(string id);
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);
        Task<ProductDTO?> UpdateAsync(string id, UpdateProductDTO dto);
        Task<bool> DeleteAsync(string id);
    }
}
