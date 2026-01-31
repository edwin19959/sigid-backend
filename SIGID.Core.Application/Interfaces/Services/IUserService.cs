//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
using SIGID.Core.Application.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request);
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request, string origin);
        Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request);
        
        //task80_edelacruz: Metodos para consulta de usuarios
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(string id);
        Task<UserDTO?> GetUserByEmailAsync(string email);
        //task80_edelacruz: Fin metodos consulta usuarios
    }
}
