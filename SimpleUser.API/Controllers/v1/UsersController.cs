using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;
using SimpleUser.API.Validators;

namespace SimpleUser.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ModelValidator]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private IValidator<UserCreateDto> _validatorCreate;
        private IValidator<UserUpdateDto> _validatorUpdate;

        public UsersController(IUserService userService, IValidator<UserCreateDto> validatorCreate,
            IValidator<UserUpdateDto> validatorUpdate)
        {
            _userService = userService;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
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

        //[HttpPost]
        //public async Task<ActionResult<PaginatedList<UserDto>>> Index(PageInfoDto pageInfo)
        //{
        //    PaginatedList<UserDto> users = await _userService.FindAllByPageAsync(pageInfo.PageSize, pageInfo.PageIndex, pageInfo.Filter);
        //    if (users == null)
        //    {
        //        return BadRequest();
        //    }
        //    return users;
        //}

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

        //// GET: Users/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            try
            {
                ValidationResult result = await _validatorCreate.ValidateAsync(userCreateDto);
                if (!result.IsValid)
                {
                    return Unauthorized(result);
                }
                return Ok(await _userService.InsertAsync(userCreateDto));
            }
            catch
            {
                return BadRequest();
            }
        }

        //// GET: Users/Edit/5
        //[HttpGet("{id}/Edit")]
        //public async Task<ActionResult<UserUpdateDto>> Edit(int id)
        //{
        //    var userUpdate = await _userService.FindUserUpdateByIdAsync(id);

        //    if (userUpdate == null)
        //    {
        //        return NotFound();
        //    }
        //    return userUpdate;
        //}

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        public async Task<ActionResult<int>> Edit(UserUpdateDto userUpdateDto)
        {
            ValidationResult result = await _validatorUpdate.ValidateAsync(userUpdateDto);

            if (!result.IsValid)
            {
                return Unauthorized();
            }
            int userId = await _userService.UpdateAsync(userUpdateDto);
            return NoContent();
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

        //// POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var userVM = await _userService.FindUserDtoByIdAsync(id);
        //    if (userVM != null)
        //    {
        //        await _userService.DeleteAsync(id);
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        //[AcceptVerbs("GET", "POST")]
        //public IActionResult IsUserAlreadyExists(string email)
        //{
        //    if (_userService.IsUserAlreadyExistsByEmail(email))
        //    {
        //        return Json($"Email {email} is already in use.");
        //    }
        //    return Json(true);
        //}
    }
}
