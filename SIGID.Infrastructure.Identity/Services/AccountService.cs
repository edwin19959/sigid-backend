using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SIGID.Core.Application.DTO.Account;
using SIGID.Core.Application.Interfaces.Services;
using SIGID.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
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

        public Task<RegisterResponseDTO> RegisterUserAsync(RegisterRequestDTO request, string origin)
        {
            //pending other developer implement
            throw new NotImplementedException();
        }

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
                new Claim("email", user.Email),
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
