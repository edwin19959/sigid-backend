//task80_edelacruz: Estructura de base de datos para usuarios de SIGID
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIGID.Core.Application.DTO.Account;
using SIGID.Core.Application.Interfaces.Services;

namespace SIGID.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;
        public AuthController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Authenticate a user and get a JWT token
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var result = await _service.LoginAsync(request);
            if (result.HasError)
                return BadRequest(result);
            return Ok(result);
        }

        //task80_edelacruz: Endpoint para registro de usuarios
        /// <summary>
        /// Register a new user in the system
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            var origin = Request.Headers["origin"].FirstOrDefault() ?? string.Empty;
            var result = await _service.RegisterAsync(request, origin);
            if (result.HasError)
                return BadRequest(result);
            return Ok(result);
        }
        //task80_edelacruz: Fin endpoint registro

        //task80_edelacruz: Endpoints para consulta de usuarios
        /// <summary>
        /// Get all users in the system
        /// </summary>
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _service.GetAllUsersAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _service.GetUserByIdAsync(id);
            if (result == null)
                return NotFound(new { message = $"Usuario con ID '{id}' no encontrado." });
            return Ok(result);
        }

        /// <summary>
        /// Get a user by email
        /// </summary>
        [HttpGet("users/email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _service.GetUserByEmailAsync(email);
            if (result == null)
                return NotFound(new { message = $"Usuario con email '{email}' no encontrado." });
            return Ok(result);
        }
        //task80_edelacruz: Fin endpoints consulta usuarios
    }
}
