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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var result = await _service.LoginAsync(request);
            return Ok(result);
        }
    }
}
