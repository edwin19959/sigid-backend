using Microsoft.AspNetCore.Identity;

namespace SIGID.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string IdentificationNumber { get; set; }
        //auditable data
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
