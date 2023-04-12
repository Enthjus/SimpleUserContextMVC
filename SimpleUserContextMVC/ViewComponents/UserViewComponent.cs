using Microsoft.AspNetCore.Mvc;
using SimpleUserContext.Models;
using SimpleUserContext.Services;
using SimpleUserContextMVC.DTOs;

namespace SimpleUserContextMVC.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        public UserViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string searchString)
        {
            if (searchString == null || searchString == "")
            {
                var userDto = await _userService.FindAllAsync();
                return View(userDto);
            }
            else
            {
                var userDto = await _userService.FindByNameAsync(searchString);
                return View(userDto);
            }
        }
    }
}
