using SIGID.Core.Application.DTO.Sales;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface ISaleService
    {
        Task<List<SaleDTO>> GetAllAsync();
        Task<SaleDTO?> GetByIdAsync(string id);
        Task<SaleDTO> CreateAsync(CreateSaleDTO dto);
        Task<bool> DeleteAsync(string id);
    }
}
