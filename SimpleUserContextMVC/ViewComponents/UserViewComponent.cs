using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Services;
using SimpleUser.MVC.ViewModels;

namespace SimpleUser.MVC.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private IUserService _userService;

        public UserViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        private int pageSize = 3;

        public async Task<IViewComponentResult> InvokeAsync(IndexVM indexVM)
        {
            PaginatedList<UserDto> pageList = await _userService.FindAllAsync(indexVM);
            UserVM userVM = new UserVM();
            userVM.Users = pageList;
            userVM.Index = indexVM;
            return View(userVM);
        }
    }
}
