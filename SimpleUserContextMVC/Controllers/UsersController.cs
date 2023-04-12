using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleUserContext.Services;
using SimpleUserContextMVC.DTOs;
using SimpleUserContextMVC.Validators;

namespace SimpleUserContextMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private IValidator<UserDto> _validator;

        public UsersController(IUserService userService, IValidator<UserDto> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        // GET: Users
        public IActionResult Index(string searchString)
        {
            //if (searchString != null)
            //{
            //    return View(searchString);
            //    RedirectToAction("Index", new { searchString = searchString });
            //}
            if(searchString != null)
            {
                return View("Index", new string($"{searchString}"));
            }
            else
            {
                return View();
            }
        }

        // GET: Users/Details/5
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

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Email,Password," +
            "UserDetailDto")] UserDto userDto)
        {
            ValidationResult result = await _validator.ValidateAsync(userDto);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(userDto);
            }
            int id = await _userService.InsertAsync(userDto);
            TempData["notice"] = "User successfully created";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userVM = await _userService.FindUserDtoByIdAsync(id.Value);

            if (userVM == null)
            {
                return NotFound();
            }
            return View(userVM);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Email,Password," +
            "UserDetailDto")] UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return NotFound();
            }
            ValidationResult result = await _validator.ValidateAsync(userDto);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View(userDto);
            }
            int userId = await _userService.UpdateAsync(userDto);
            return RedirectToAction(nameof(Details), new { id = userId });
        }

        // GET: Users/Delete/5
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

        // POST: Users/Delete/5
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
