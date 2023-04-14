using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.API.DTOs;
using SimpleUser.API.Services;
using SimpleUser.API.Validators;

namespace SimpleUser.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<PaginatedList<UserDto>>> Index()
        {
            PaginatedList<UserDto> users = await _userService.FindAllByPageAsync(0, 0, "");
            if (users == null)
            {
                return NotFound();
            }
            return users;
        }

        [HttpPost]
        public async Task<ActionResult<PaginatedList<UserDto>>> Index(PageInfoDto pageInfo)
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
            if (id == null)
            {
                return NotFound();
            }

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

        //// POST: Users/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Username,Email,Password,ConfirmPassword," +
        //    "UserDetailDto")] UserCreateDto userCreateDto)
        //{
        //    ValidationResult result = await _validatorCreate.ValidateAsync(userCreateDto);
        //    if (!result.IsValid)
        //    {
        //        result.AddToModelState(this.ModelState);
        //        return View(userCreateDto);
        //    }
        //    int id = await _userService.InsertAsync(userCreateDto);
        //    return RedirectToAction(nameof(Details), new { id = id });
        //}

        //// GET: Users/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userVM = await _userService.FindUserUpdateByIdAsync(id.Value);

        //    if (userVM == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userVM);
        //}

        //// POST: Users/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email," +
        //    "UserDetailDto")] UserUpdateDto userUpdateDto)
        //{
        //    if (id != userUpdateDto.Id)
        //    {
        //        return NotFound();
        //    }
        //    ValidationResult result = await _validatorUpdate.ValidateAsync(userUpdateDto);

        //    if (!result.IsValid)
        //    {
        //        result.AddToModelState(this.ModelState);
        //        return View(userUpdateDto);
        //    }
        //    int userId = await _userService.UpdateAsync(userUpdateDto);
        //    return RedirectToAction(nameof(Details), new { id = userId });
        //}

        //// GET: Users/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    UserDto userVM = await _userService.FindUserDtoByIdAsync(id.Value);

        //    if (userVM == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userVM);
        //}

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
