using Microsoft.AspNetCore.Identity;

namespace WebApiOnionArchitecture.Domain.Entities.Identity
{
    public class AppUser:IdentityUser
    {
        public string? Fullname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
