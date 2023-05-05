using Microsoft.AspNetCore.Mvc;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;
using System.Security.Claims;

namespace SimpleUser.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
            var result = await _accountService.SignInAsync(signInDto);
            if(result == null)
            {
                return Unauthorized("Email or password is incorrect.");
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile(UserProfileDto userProfile)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }
            var currentUser = GetCurrentUser();
            if(currentUser == null)
            {
                return Unauthorized("Please login.");
            }
            var result = await _accountService.UpdateUserProfile(currentUser.Email, userProfile);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }
            var currentUser = GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized("Please login.");
            }
            var result = await _accountService.ChangePassword(currentUser.Email, changePassword);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        private ClaimDto GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;

                return new ClaimDto
                {
                    Email = userClaims.FirstOrDefault(u => u.Type == "Email")?.Value,
                    Roles = userClaims.Where(u => u.Type == "Role").ToList()
                };
            }
            return null;
        }
    }
}
