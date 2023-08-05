using Microsoft.AspNetCore.Mvc;
using WebApiOnionArchitecture.Application.Abstraction.Services;
using WebApiOnionArchitecture.Application.DTOs.Auth_DTOs;

namespace WebApiOnionAchitecture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            await _authService.Register(userRegisterDto);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(SignInDto signInDto)
        {
           TokenResponseDto tokenResponse = await _authService.Login(signInDto);
            return Ok(tokenResponse);
        }
    }
}
