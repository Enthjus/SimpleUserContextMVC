using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.Services;

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

        public async Task<IViewComponentResult> InvokeAsync(IndexViewModel indexVM)
        {
            PaginatedList<CustomerDto> pageList = await _userService.FindAllAsync(indexVM);
            CustomerViewModel userVM = new CustomerViewModel();
            userVM.Customers = pageList;
            userVM.Index = indexVM;
            return View(userVM);
        }
    }
}
