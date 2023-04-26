using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;

namespace SimpleUser.API.Controllers.v1
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ModelValidator]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserDto>>> Index([FromQuery] PageInfoDto pageInfo)
        {
            PaginatedList<UserDto> users = await _userService.FindAllByPageAsync(pageInfo.PageSize, pageInfo.PageIndex, pageInfo.Filter);
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Details(int id)
        {
            UserDto userDto = await _userService.FindUserDtoByIdAsync(id);

            if (userDto == null)
            {
                return NotFound();
            }

            return userDto;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }
            return Ok(await _userService.InsertAsync(userCreateDto));
        }

        [HttpPut]
        public async Task<ActionResult<int>> Edit(UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized(ModelState);
            }
            return Ok(await _userService.UpdateAsync(userUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
