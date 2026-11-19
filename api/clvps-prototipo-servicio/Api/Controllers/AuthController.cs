using clvps_prototipo_servicio.Api.DTO;
using clvps_prototipo_servicio.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clvps_prototipo_servicio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResponse result = await _authService.Login(request);

            if (result.Ok)
            {
                return Ok(new {
                    result.UserName,
                    result.NombreCompleto,
                    result.Rol
                });
            }

            return Unauthorized(new { result.ErrorMessage });
        }
    }
}
