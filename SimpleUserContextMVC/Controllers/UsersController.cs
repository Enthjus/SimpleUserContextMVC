using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Services;
using SimpleUser.MVC.Validators;
using SimpleUser.MVC.ViewModels;

namespace SimpleUser.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private IValidator<UserCreateDto> _validatorCreate;
        private IValidator<UserUpdateDto> _validatorUpdate;

        public UsersController(IUserService userService, IValidator<UserCreateDto> validatorCreate, IValidator<UserUpdateDto> validatorUpdate)
        {
            _userService = userService;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        public IActionResult Index(IndexVM indexVM)
        {
            if (string.IsNullOrEmpty(indexVM.Filter))
            {
                indexVM.Filter = "";
            }
            if (_userService.IsNullOrZero(indexVM.PageIndex))
            {
                indexVM.PageIndex = 1;
            }
            if (_userService.IsNullOrZero(indexVM.PageSize))
            {
                indexVM.PageSize = 4;
            }
            return View("Index", indexVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDto userVM = await _userService.FindUserDtoByIdAsync(id.Value);

            if (userVM == null)
            {
                return NotFound();
            }

            return View(userVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password,ConfirmPassword," +
            "UserDetailDto")] UserCreateDto userCreateDto)
        {
            var result = _validatorCreate.Validate(userCreateDto);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(userCreateDto);
            }
            ValidationErrorDto apiResult = await _userService.InsertAsync(userCreateDto);
            if(apiResult.status == 400)
            {
                apiResult.AddToModelState(this.ModelState);
                return View(userCreateDto);
            }
            return RedirectToAction("Details", new { id = apiResult.Id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVM = await _userService.FindUserUpdateByIdAsync(id.Value);

            if (userVM == null)
            {
                return NotFound();
            }
            return View(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,OldPassword,NewPassword,ConfirmNewPassword," +
            "UserDetailDto")] UserUpdateDto userUpdateDto)
        {
            if (id != userUpdateDto.Id)
            {
                return NotFound();
            }
            var result = _validatorUpdate.Validate(userUpdateDto);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(userUpdateDto);
            }
            ValidationErrorDto apiResult = await _userService.UpdateAsync(userUpdateDto);
            if (apiResult.status == 400)
            {
                apiResult.AddToModelState(this.ModelState);
                return View(userUpdateDto);
            }
            return RedirectToAction("Details", new { id = apiResult.Id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserDto userVM = await _userService.FindUserDtoByIdAsync(id.Value);

            if (userVM == null)
            {
                return NotFound();
            }

            return View(userVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userVM = await _userService.FindUserDtoByIdAsync(id);
            if (userVM != null)
            {
                await _userService.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
