using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Services;
using SimpleUser.MVC.ViewModels;

namespace SimpleUser.MVC.ViewComponents
{
    public class CustomerViewComponent : ViewComponent
    {
        private ICustomerService _userService;

        public CustomerViewComponent(ICustomerService userService)
        {
            _userService = userService;
        }
        private int pageSize = 3;

        public async Task<IViewComponentResult> InvokeAsync(IndexVM indexVM)
        {
            PaginatedList<CustomerDto> pageList = await _userService.FindAllAsync(indexVM);
            CustomerVM userVM = new CustomerVM();
            userVM.Users = pageList;
            userVM.Index = indexVM;
            return View(userVM);
        }
    }
}
