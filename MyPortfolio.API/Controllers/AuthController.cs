using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Services;
using MyPortfolio.Shared.DTOs;
using RESTFulSense.Controllers;

namespace MyPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : RESTFulController
    {
        private readonly IAuthService auth;

        public AuthController(IAuthService auth)
        {
            this.auth = auth;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto dto)
        {
            var token = auth.Authenticate(dto.Username, dto.Password);
            if (token == null)
                return Unauthorized();

            return Ok(new { token });
        }
    }

}
