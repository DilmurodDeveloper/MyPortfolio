using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Services.Auth;
using MyPortfolio.Shared.DTOs.Login;
using RESTFulSense.Controllers;

namespace MyPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : RESTFulController
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var result = authService.Login(dto);

            if (result is null)
            {
                return Unauthorized(
                    new
                    {
                        message = "Invalid username or password"
                    });
            }

            return Created(result);
        }
    }
}
