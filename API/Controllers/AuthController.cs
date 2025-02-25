using Application.DTOs;
using Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var Result = await _userService.RegisterUser(registerDTO);
            if (!Result.IsSuccess)
            {
                return BadRequest(new { message = Result.ErrorMessage });
            }

            return Ok(new
            {
                Username = Result.Username,
                Email = Result.Email,
                Mobile = Result.Mobile,
                Token = Result.Token
            });
        }
        [Route("LoginByEmail")]
        [HttpPost]
        public async Task<IActionResult> LoginByEmail([FromBody] LoginByEmail dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Please fill all the required properties" });
            }
            var res = await _userService.LoginByEmail(dto);
            if (!res.IsSuccess)
            {
                return BadRequest(new { message = res.ErrorMessage });
            }
            return Ok(new
            {
                Username = res.Username,
                Email = res.Email,
                Mobile = res.Mobile,
                Token = res.Token
            });
        }
    }
}
