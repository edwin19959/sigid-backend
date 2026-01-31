using Microsoft.AspNetCore.Identity;

namespace SIGID.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public required string IdentificationNumber { get; set; }
        
        //auditable data
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
