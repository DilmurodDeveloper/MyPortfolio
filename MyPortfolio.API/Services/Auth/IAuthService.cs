using MyPortfolio.Shared.DTOs.Login;

namespace MyPortfolio.API.Services.Auth
{
    public interface IAuthService
    {
        LoginResponseDto? Login(LoginDto dto);
    }
}
