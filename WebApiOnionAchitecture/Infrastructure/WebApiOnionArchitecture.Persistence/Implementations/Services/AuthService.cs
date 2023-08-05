using Microsoft.AspNetCore.Identity;
using System.Text;
using WebApiOnionArchitecture.Application.Abstraction.Services;
using WebApiOnionArchitecture.Application.DTOs.Auth_DTOs;
using WebApiOnionArchitecture.Domain.Entities.Identity;
using WebApiOnionArchitecture.Domain.Enums;
using WebApiOnionArchitecture.Persistence.Exceptions;

namespace WebApiOnionArchitecture.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<TokenResponseDto> Login(SignInDto signInDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(signInDto.UsernameOrEmail);
            if (appUser is null)
            {
                appUser = await _userManager.FindByNameAsync(signInDto.UsernameOrEmail);
                if (appUser is null)
                {
                    throw new SignInFailureException("Sign in failed");
                }
            }
            SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, signInDto.password, true);
            if (!signInResult.Succeeded) { throw new SignInFailureException("Sign in failed"); }

            TokenResponseDto tokenResponse = _jwtService.CreateJwtToken(appUser);
            appUser.RefreshToken = tokenResponse.refreshToken;
            appUser.RefreshTokenExpiration = tokenResponse.refreshTokenExpiration;
            await _userManager.UpdateAsync(appUser);
            return tokenResponse;
        }

        public async Task Register(UserRegisterDto userRegisterDto)
        {
            AppUser newUser = new()
            {
                Fullname = userRegisterDto.fullname,
                UserName = userRegisterDto.username,
                Email = userRegisterDto.email,
            };
            IdentityResult identityResult = await _userManager.CreateAsync(newUser, userRegisterDto.password);
            if (!identityResult.Succeeded)
            {
                StringBuilder errorMessage = new();
                foreach (var error in identityResult.Errors) { errorMessage.AppendLine(error.Description); }
                throw new UserRegistrationException(errorMessage.ToString());
            }
            var result = await _userManager.AddToRoleAsync(newUser, Role.Member.ToString());
            if (!result.Succeeded)
            {
                StringBuilder errorMessage = new();
                foreach (var error in result.Errors)
                {
                    errorMessage.AppendLine(error.Description);
                }
                throw new UserRegistrationException(errorMessage.ToString());
            }
        }
    }
}
