using SIGID.Core.Application.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request);
        Task<string> ConfirmAccountAsync(string userId, string token);

        //maybe in the future can be used
        //Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponseDTO> RegisterUserAsync(RegisterRequestDTO request, string origin);
        Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request);
    }
}
