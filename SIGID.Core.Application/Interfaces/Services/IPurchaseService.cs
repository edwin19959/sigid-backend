using SIGID.Core.Application.DTO.Purchases;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IPurchaseService
    {
        Task<List<PurchaseDTO>> GetAllAsync();
        Task<PurchaseDTO?> GetByIdAsync(string id);
        Task<PurchaseDTO> CreateAsync(CreatePurchaseDTO dto);
        Task<bool> DeleteAsync(string id);
    }
}
