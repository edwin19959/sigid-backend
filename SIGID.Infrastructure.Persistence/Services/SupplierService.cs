using Microsoft.EntityFrameworkCore;
using SIGID.Core.Application.DTO.Suppliers;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Core.Domain.Entities;

namespace SIGID.Infrastructure.Persistence.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SupplierDTO>> GetAllAsync()
        {
            return await _context.Suppliers.Select(s => new SupplierDTO
            {
                Id = s.Id, CompanyName = s.CompanyName, Rnc = s.Rnc,
                Email = s.Email, Phone = s.Phone, Address = s.Address, Status = s.Status
            }).ToListAsync();
        }

        public async Task<SupplierDTO?> GetByIdAsync(string id)
        {
            var s = await _context.Suppliers.FindAsync(id);
            if (s == null) return null;
            return new SupplierDTO { Id = s.Id, CompanyName = s.CompanyName, Rnc = s.Rnc, Email = s.Email, Phone = s.Phone, Address = s.Address, Status = s.Status };
        }

        public async Task<SupplierDTO> CreateAsync(CreateSupplierDTO dto)
        {
            var supplier = new Supplier
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = dto.CompanyName, Rnc = dto.Rnc,
                Email = dto.Email, Phone = dto.Phone,
                Address = dto.Address, Status = dto.Status
            };
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return new SupplierDTO { Id = supplier.Id, CompanyName = supplier.CompanyName, Rnc = supplier.Rnc, Email = supplier.Email, Phone = supplier.Phone, Address = supplier.Address, Status = supplier.Status };
        }

        public async Task<SupplierDTO?> UpdateAsync(string id, UpdateSupplierDTO dto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return null;
            supplier.CompanyName = dto.CompanyName;
            supplier.Rnc = dto.Rnc;
            supplier.Email = dto.Email;
            supplier.Phone = dto.Phone;
            supplier.Address = dto.Address;
            supplier.Status = dto.Status;
            await _context.SaveChangesAsync();
            return new SupplierDTO { Id = supplier.Id, CompanyName = supplier.CompanyName, Rnc = supplier.Rnc, Email = supplier.Email, Phone = supplier.Phone, Address = supplier.Address, Status = supplier.Status };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return false;
            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
