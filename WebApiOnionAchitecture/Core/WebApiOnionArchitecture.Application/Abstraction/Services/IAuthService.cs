using WebApiOnionArchitecture.Application.DTOs.Auth_DTOs;

namespace WebApiOnionArchitecture.Application.Abstraction.Services
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto userRegisterDto);
        Task<TokenResponseDto> Login(SignInDto signInDto);
    }
}
