using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.Services;
using SimpleUser.Persistence.Data;
using System.Net.Http.Headers;

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
