using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.Core;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Services;
using SimpleUser.MVC.Validators;
using SimpleUser.MVC.ViewModels;
using static SimpleUser.MVC.Core.Constants;

namespace SimpleUser.MVC.Controllers
{
    public class CustomerController : Controller
    {
        #region CallApi
        private readonly ICustomerService _userService;
        private IValidator<CustomerCreateDto> _validatorCreate;
        private IValidator<CustomerUpdateDto> _validatorUpdate;

        public CustomerController(ICustomerService userService, IValidator<CustomerCreateDto> validatorCreate, IValidator<CustomerUpdateDto> validatorUpdate)
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

            CustomerDto userVM = await _userService.FindUserDtoByIdAsync(id.Value);

            if (userVM == null)
            {
                return NotFound();
            }

            return View(userVM);
        }

        [Authorize(Roles = $"{Constants.Roles.Manager},{Constants.Roles.Administrator}")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = $"{Constants.Roles.Manager},{Constants.Roles.Administrator}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password,ConfirmPassword," +
            "CustomerDetailDto")] CustomerCreateDto userCreateDto)
        {
            var result = _validatorCreate.Validate(userCreateDto);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(userCreateDto);
            }
            ValidationErrorDto apiResult = await _userService.InsertAsync(userCreateDto);
            if (apiResult.status == 400)
            {
                apiResult.AddToModelState(this.ModelState);
                return View(userCreateDto);
            }
            return RedirectToAction("Details", new { id = apiResult.Id });
        }

        [Authorize(Policy = Constants.Policies.SuperAdmin)]
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

        [Authorize(Policy = Constants.Policies.SuperAdmin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,OldPassword,NewPassword,ConfirmNewPassword," +
            "CustomerDetailDto")] CustomerUpdateDto userUpdateDto)
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

        [Authorize(Policy = Constants.Policies.CanDeleteUser)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CustomerDto userVM = await _userService.FindUserDtoByIdAsync(id.Value);

            if (userVM == null)
            {
                return NotFound();
            }

            return View(userVM);
        }

        [Authorize(Policy = Constants.Policies.CanDeleteUser)]
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
        #endregion
    }
}
