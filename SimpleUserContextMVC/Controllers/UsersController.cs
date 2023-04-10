using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpleUserContext.Services;
using SimpleUserContextMVC.DTOs;

namespace SimpleUserContextMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            IList<UserDto> userVMs = await _userService.FindAllAsync();

            if(userVMs == null)
            {
                return Problem("Entity set 'ApplicationContext.Users'  is null.");
            }

            return View(userVMs);
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
            if (ModelState.IsValid)
            {
                int id = await _userService.InsertAsync(userDto);
                return RedirectToAction(nameof(Details), new { id = id });
            }
            return View(userDto);
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

            if (ModelState.IsValid)
            {
                int userId = await _userService.UpdateAsync(userDto);
                return RedirectToAction(nameof(Details), new {id = userId});
            }
            return View(userDto);
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
