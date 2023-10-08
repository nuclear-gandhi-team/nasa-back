using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO;
using Nasa.Common.DTO.User;

namespace Nasa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AccountController(IUserService userService, IMapper mapper, IAuthService authService)
        {
            _userService = userService;
            _mapper = mapper;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<AuthorizationResponse>> RegisterUser(RegisterUserDto userDto)
        {
            await _userService.CreateUserAsync(userDto);

            var login = _mapper.Map<LoginUserDto>(userDto);

            var response = await _authService.Authorize(login);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthorizationResponse>> Login(LoginUserDto dto)
        {
            return Ok(await _authService.Authorize(dto));
        }
    }
}
