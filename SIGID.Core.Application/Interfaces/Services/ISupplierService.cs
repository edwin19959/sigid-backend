using SIGID.Core.Application.DTO.Suppliers;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface ISupplierService
    {
        Task<List<SupplierDTO>> GetAllAsync();
        Task<SupplierDTO?> GetByIdAsync(string id);
        Task<SupplierDTO> CreateAsync(CreateSupplierDTO dto);
        Task<SupplierDTO?> UpdateAsync(string id, UpdateSupplierDTO dto);
        Task<bool> DeleteAsync(string id);
    }
}
