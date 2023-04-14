using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.Services;
using SimpleUser.Persistence.Data;

namespace SimpleUser.MVC.ViewComponents
{
    public class UserViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public UserViewComponent(IUserService userService, ApplicationContext context, IMapper mapper)
        {
            _userService = userService;
            _context = context;
            _mapper = mapper;
        }
        private int pageSize = 3;

        public async Task<IViewComponentResult> InvokeAsync(string searchString, int pageIndex)
        {
            IQueryable<User> users = from u in _context.Users.Include(x => x.UserDetail)
                                     select u;
            //IQueryable<UserDto> userDTOs = _mapper.Map<IQueryable<UserDto>>(users);
            IList<UserDto> userDtos;
            PaginatedList<User> pageList;
            if (!string.IsNullOrEmpty(searchString)) // TODO: to be replaced with built-in function
            {
                users = users
                    .Where(u => u.UserDetail.LastName.ToUpper().Contains(searchString.ToUpper()) ||
                    u.UserDetail.FirstName.ToUpper().Contains(searchString.ToUpper()) ||
                    u.UserDetail.PhoneNumber.Contains(searchString) ||
                    u.Email.ToUpper().Contains(searchString.ToUpper()));
            }
            pageList = await PaginatedList<User>.CreateAsync(users, pageIndex, pageSize);
            userDtos = _mapper.Map<List<UserDto>>(pageList.ToList());
            var userVM = new UserVM();
            userVM.Users = userDtos;
            userVM.SearchString = searchString;
            userVM.PageIndex = pageIndex;
            userVM.HasPreviousPage = pageList.HasPreviousPage;
            userVM.HasNextPage = pageList.HasNextPage;
            return View(userVM);
        }
    }
}
