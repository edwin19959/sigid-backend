//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIGID.Core.Application.DTO.Account;
using SIGID.Core.Application.Enums;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SIGID.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountService(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public Task<string> ConfirmAccountAsync(string userId, string token)
        {
            //pending other developer implement
            throw new NotImplementedException();
        }

        //this method works for user can be authenticated
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            LoginResponseDTO response = new();

            //for sure nobody has the same email or email should be UNIQUE. Identity has that constraint
            var user = await _userManager.FindByEmailAsync(request.Email);

            //if user is null, it means that email does not exists
            if(user is null)
            {
                response.HasError = true;
                response.Error = "No se reconoce el email con el que intenta iniciar sesion. Intentelo de nuevo";
                return response;
            }

            // compare password, if password are match, then continue
            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            
            //in case of any error the credentials are wrong
            if (!validPassword)
            {
                response.HasError = true;
                response.Error = "Credenciales invalidas.";
                return response;
            }

            // the account is not active
            if (!user.EmailConfirmed)
            {
                response.HasError= true;
                response.Error = "Esta cuenta no esta verificada. Por favor contacte con su administrador";
                return response;
            }

            // map manually the entity
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = roleList.ToList();
            response.IsActive = user.EmailConfirmed;
            response.token = await JwtTokenGenerator(user);

            return response;
        }

        //task80_edelacruz: Implementacion de registro de usuarios en el sistema
        public async Task<RegisterResponseDTO> RegisterUserAsync(RegisterRequestDTO request, string origin)
        {
            RegisterResponseDTO response = new();

            // Validate passwords match
            if (request.Password != request.ConfirmPassword)
            {
                response.HasError = true;
                response.Error = "Las contraseñas no coinciden.";
                return response;
            }

            // Check if email already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                response.HasError = true;
                response.Error = $"El email '{request.Email}' ya está registrado.";
                return response;
            }

            // Validate the role exists
            var validRoles = Enum.GetNames(typeof(Roles));
            if (!validRoles.Contains(request.Role, StringComparer.OrdinalIgnoreCase))
            {
                response.HasError = true;
                response.Error = $"El rol '{request.Role}' no es válido. Roles disponibles: {string.Join(", ", validRoles)}";
                return response;
            }

            // Create new user
            var newUser = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                IdentificationNumber = request.IdentificationNumber,
                EmailConfirmed = true, // Auto-confirm for now
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = string.Join(", ", result.Errors.Select(e => e.Description));
                return response;
            }

            // Assign role to user
            await _userManager.AddToRoleAsync(newUser, request.Role);

            // Map response
            response.Id = newUser.Id;
            response.Name = newUser.Name;
            response.LastName = newUser.LastName;
            response.Email = newUser.Email!;
            response.Role = request.Role;
            response.IsActive = newUser.IsActive;
            response.IdentificationNumber = newUser.IdentificationNumber;
            response.HasError = false;

            return response;
        }
        //task80_edelacruz: Fin implementacion registro de usuarios

        //task80_edelacruz: Implementacion de consulta de usuarios
        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDTOs = new List<UserDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDTOs.Add(new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email ?? string.Empty,
                    IdentificationNumber = user.IdentificationNumber,
                    Roles = roles.ToList(),
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                });
            }

            return userDTOs;
        }

        public async Task<UserDTO?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                IdentificationNumber = user.IdentificationNumber,
                Roles = roles.ToList(),
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                IdentificationNumber = user.IdentificationNumber,
                Roles = roles.ToList(),
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
        //task80_edelacruz: Fin implementacion consulta de usuarios

        //this method works for reset password in case an user on the lab lost his account access
        public async Task<ResetPasswordResponseDTO> ResetPasswordAsync(ResetPasswordRequestDTO request)
        {
            ResetPasswordResponseDTO response = new()
            {
                HasError = true
            };

            //find an user by email
            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user is null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email}, nunca ha sido registrado.";
                return response;
            }
            
            //generate token for reset password
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Ha ocurrido un error mientras se reiniciaba la contraseña.";
                return response;
            }

            return response;
        }


        private async Task<TokenResponseDTO> JwtTokenGenerator(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("email", user.Email!),
                new Claim("id", user.Id)
            };

            //get aditional claims like roles 
            var claimDB = await _userManager.GetClaimsAsync(user);
            claims.AddRange(claimDB);

            //set token sign
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwtKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //token will be valid for 3 hours
            var expiration = DateTime.UtcNow.AddHours(3);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new TokenResponseDTO
            {
                Token = token,
                Expiration = expiration
            };
        }
    }
}
