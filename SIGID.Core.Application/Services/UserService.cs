//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
using SIGID.Core.Application.DTO.Account;
using SIGID.Core.Application.Interfaces.Services;

namespace SIGID.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        public UserService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public Task<string> ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            LoginResponseDTO userResponse = await _accountService.LoginAsync(request);
            return userResponse;
        }

        //task80_edelacruz: Implementacion de registro de usuarios
        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request, string origin)
        {
            return await _accountService.RegisterUserAsync(request, origin);
        }
        //task80_edelacruz: Fin implementacion registro

        public async Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            return await _accountService.ResetPasswordAsync(request);
        }

        //task80_edelacruz: Implementacion de consulta de usuarios
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            return await _accountService.GetAllUsersAsync();
        }

        public async Task<UserDTO?> GetUserByIdAsync(string id)
        {
            return await _accountService.GetUserByIdAsync(id);
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            return await _accountService.GetUserByEmailAsync(email);
        }
        //task80_edelacruz: Fin implementacion consulta usuarios
    }
}
