//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
namespace SIGID.Core.Application.DTO.Account
{
    //task80_edelacruz: DTO para registro de usuarios con Email agregado
    public class RegisterRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
    //task80_edelacruz: Fin DTO registro
}
