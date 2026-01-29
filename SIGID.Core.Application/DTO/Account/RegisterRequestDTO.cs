using SIGID.Core.Domain.Common;


namespace SIGID.Core.Application.DTO.Account
{
    public class RegisterRequestDTO : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string IdentificationNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
