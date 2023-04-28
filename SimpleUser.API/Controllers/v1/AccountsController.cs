using Microsoft.AspNetCore.Mvc;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;

namespace SimpleUser.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }
            var result = await _accountService.SignUpAsync(signUpDto);
            if (result.Succeeded)
            {
                return Ok("Successful create account.");
            }
            return BadRequest(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDto signInDto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }
            var result = _accountService.SignInAsync(signInDto);
            if(result is null)
            {
                return Unauthorized("Email or password is incorrect.");
            }
            return Ok(result);
        }
    }
}
