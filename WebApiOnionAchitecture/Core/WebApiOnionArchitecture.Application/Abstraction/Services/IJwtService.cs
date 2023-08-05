using WebApiOnionArchitecture.Application.DTOs.Auth_DTOs;
using WebApiOnionArchitecture.Domain.Entities.Identity;

namespace WebApiOnionArchitecture.Application.Abstraction.Services
{
    public interface IJwtService
    {
        TokenResponseDto CreateJwtToken(AppUser appUser);
    }
}
