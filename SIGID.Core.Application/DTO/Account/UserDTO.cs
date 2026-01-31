//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
namespace SIGID.Core.Application.DTO.Account
{
    //task80_edelacruz: DTO para consulta de usuarios
    public class UserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    //task80_edelacruz: Fin DTO consulta usuarios
}
