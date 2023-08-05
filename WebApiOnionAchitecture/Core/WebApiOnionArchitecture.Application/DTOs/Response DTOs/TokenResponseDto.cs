namespace WebApiOnionArchitecture.Application.DTOs.Auth_DTOs
{
    public record TokenResponseDto(string token, DateTime expireDate, DateTime refreshTokenExpiration, string refreshToken);
}
