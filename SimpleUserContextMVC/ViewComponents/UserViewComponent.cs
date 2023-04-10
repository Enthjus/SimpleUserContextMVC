using Microsoft.AspNetCore.Mvc;
using SimpleUserContext.Models;
using SimpleUserContextMVC.DTOs;

namespace SimpleUserContextMVC.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        public UserViewComponent() { }

        public IViewComponentResult Invoke(UserDto userDto)
        {
            return View(userDto);
        }
    }
}
