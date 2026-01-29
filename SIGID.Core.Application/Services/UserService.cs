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

        //in identity's layer pending implement those methos
        public Task<string> ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            LoginResponseDTO userResponse = await _accountService.LoginAsync(request);
            return userResponse;
        }

        public Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request, string origin)
        {
            throw new NotImplementedException();
        }

        public async Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            return await _accountService.ResetPasswordAsync(request);
        }
    }
}
