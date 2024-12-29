using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerDTO)
        {
            var Result = await _userService.RegisterUser(registerDTO);
            if (!Result.IsSuccess)
            {
                return BadRequest(Result.ErrorMessage);
            }

            return Ok(Result);
        }
    }
}
