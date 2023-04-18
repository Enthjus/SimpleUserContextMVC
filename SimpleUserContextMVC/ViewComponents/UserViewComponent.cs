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
        public UserViewComponent()
        {
        }
        private int pageSize = 3;

        public async Task<IViewComponentResult> InvokeAsync(IndexVM indexVM)
        {
            //IQueryable<User> users = from u in _context.Users.Include(x => x.UserDetail)
            //                         select u;
            ////IQueryable<UserDto> userDTOs = _mapper.Map<IQueryable<UserDto>>(users);
            //IList<UserDto> userDtos;
            PaginatedList<UserDto> pageList = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7037/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync(
                    $"api/v1/Users?PageSize={indexVM.PageSize}&PageIndex={indexVM.PageIndex}&Filter={indexVM.Filter}");
                if (response.IsSuccessStatusCode)
                {
                    pageList = await response.Content.ReadFromJsonAsync<PaginatedList<UserDto>>();
                }
            }
            UserVM userVM = new UserVM();
            userVM.Users = pageList;
            userVM.Index = indexVM;
            //if (!string.IsNullOrEmpty(searchString)) // TODO: to be replaced with built-in function
            //{
            //    users = users
            //        .Where(u => u.UserDetail.LastName.ToUpper().Contains(searchString.ToUpper()) ||
            //        u.UserDetail.FirstName.ToUpper().Contains(searchString.ToUpper()) ||
            //        u.UserDetail.PhoneNumber.Contains(searchString) ||
            //        u.Email.ToUpper().Contains(searchString.ToUpper()));
            //}

            //pageList = await PaginatedList<UserDto>.CreateAsync(users, pageIndex, pageSize);
            //userDtos = _mapper.Map<List<UserDto>>(pageList.ToList());
            //var userVM = new UserVM();
            //userVM.Users = userDtos;
            //userVM.SearchString = searchString;
            //userVM.PageIndex = pageIndex;
            //userVM.HasPreviousPage = pageList.HasPreviousPage;
            //userVM.HasNextPage = pageList.HasNextPage;
            return View(userVM);
        }
    }
}
