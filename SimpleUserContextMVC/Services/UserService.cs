using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.DTOs;
using SimpleUser.Persistence.Data;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace SimpleUser.MVC.Services
{
    public class UserService : IUserService
    {
        //private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private HttpClient _httpClient;

        public UserService(HttpClient httpClient, IMapper mapper)
        {
            //_context = context;
            //_mapper = mapper;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7037/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/v1/Users/{id}");
        }

        //public async Task<PaginatedList<UserDto>> FindAllAsync()
        //{
        //    HttpResponseMessage response = await _httpClient.GetAsync(
        //            @"Users?PageSize=10&PageIndex=1&Filter=""");
        //    PaginatedList<UserDto> pageList = await response.Content.ReadFromJsonAsync<PaginatedList<UserDto>>();
        //    return pageList;
        //}

        //public async Task<User> FindByIdAsync(int id)
        //{
        //    User user = await _context.Users.Include(x => x.UserDetail).FirstOrDefaultAsync(u => u.Id == id);
        //    return user;
        //}

        //public async Task<IList<UserDto>> FindAsync(string keyword)
        //{
        //    var users = await _context.Users.Include(x => x.UserDetail)
        //        .Where(u => u.UserDetail.LastName.ToUpper().Contains(keyword.ToUpper()) ||
        //        u.UserDetail.FirstName.ToUpper().Contains(keyword.ToUpper()) ||
        //        u.UserDetail.PhoneNumber.Contains(keyword) ||
        //        u.Email.ToUpper().Contains(keyword.ToUpper()))
        //        .ToListAsync();
        //    IList<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
        //    return userDtos;
        //}

        public async Task<UserDto> FindUserDtoByIdAsync(int id)
        {
            // New code:
            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/Users/{id}");
            UserDto userDto = null;
            if (response.IsSuccessStatusCode)
            {
                userDto = await response.Content.ReadFromJsonAsync<UserDto>();
            }
            //User user = await FindByIdAsync(id);
            //UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<int> InsertAsync(UserCreateDto userCreateDto)
        {
            //var content = new StringContent(userCreateDto.ToString(), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/v1/Users", userCreateDto);
            var result = httpResponseMessage.Content.ReadAsStringAsync();
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return int.Parse(await httpResponseMessage.Content.ReadAsStringAsync());
            }
            else
            {
                return 0;
            }
            //User user = _mapper.Map<User>(userCreateDto);
            //var entry = _context.Users.Add(user);
            //await _context.SaveChangesAsync();
            //entry.Entity.Id;
        }

        public async Task<int> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/v1/Users", userUpdateDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            //var oldUser = await FindByIdAsync(userUpdateDto.Id);
            //var entry = _context.Users.Update(oldUser);
            //entry.CurrentValues.SetValues(userUpdateDto);
            //await _context.SaveChangesAsync();
            //return entry.Entity.Id;
        }

        public async Task<UserUpdateDto> FindUserUpdateByIdAsync(int id)
        {
            var user = await FindUserDtoByIdAsync(id);
            UserUpdateDto userUpdate = _mapper.Map<UserUpdateDto>(user);
            return userUpdate;
        }

        //public bool IsUserAlreadyExistsByEmail(string email)
        //{
        //    return _context.Users.Any(x => x.Email == email);
        //}

        public bool IsNullOrZero(int num)
        {
            return num == 0 || num == null;
        }
    }
}
