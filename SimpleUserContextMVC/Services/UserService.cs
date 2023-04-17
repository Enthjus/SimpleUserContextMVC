using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.DTOs;
using SimpleUser.Persistence.Data;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace SimpleUser.MVC.Services
{
    public class UserService : IUserService
    {
        //private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private HttpClient _httpClient;

        public UserService(ApplicationContext context, IMapper mapper, HttpClient httpClient)
        {
            //_context = context;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task DeleteAsync(int id)
        {
            //User user = await FindByIdAsync(id);
            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
        }

        public async Task<IList<UserDto>> FindAllAsync()
        {
            //var users = await _context.Users.Include(x => x.UserDetail).AsNoTracking().ToListAsync();
            //var userDtos = _mapper.Map<List<UserDto>>(users);
            return null;
        }

        public async Task<User> FindByIdAsync(int id)
        {
            //User user = await _context.Users.Include(x => x.UserDetail).FirstOrDefaultAsync(u => u.Id == id);
            return null;
        }

        public async Task<IList<UserDto>> FindAsync(string keyword)
        {
            //var users = await _context.Users.Include(x => x.UserDetail)
            //    .Where(u => u.UserDetail.LastName.ToUpper().Contains(keyword.ToUpper()) ||
            //    u.UserDetail.FirstName.ToUpper().Contains(keyword.ToUpper()) ||
            //    u.UserDetail.PhoneNumber.Contains(keyword) ||
            //    u.Email.ToUpper().Contains(keyword.ToUpper()))
            //    .ToListAsync();
            //IList<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
            return null;
        }

        public async Task<UserDto> FindUserDtoByIdAsync(int id)
        {
            _httpClient.BaseAddress = new Uri("http://localhost:7037/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // New code:
            HttpResponseMessage response = await _httpClient.GetAsync("api/Users/1");
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
            //GET Method
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/Users", JsonConvert.SerializeObject(userCreateDto));
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return 1;
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
            //var oldUser = await FindByIdAsync(userUpdateDto.Id);
            //var entry = _context.Users.Update(oldUser);
            //entry.CurrentValues.SetValues(userUpdateDto);
            //await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<UserUpdateDto> FindUserUpdateByIdAsync(int id)
        {
            var user = await FindByIdAsync(id);
            UserUpdateDto userUpdate = _mapper.Map<UserUpdateDto>(user);
            return userUpdate;
        }

        public bool IsUserAlreadyExistsByEmail(string email)
        {
            return true; /*_context.Users.Any(x => x.Email == email);*/
        }

        public bool IsNullOrZero(int num)
        {
            return num == 0 || num == null;
        }
    }
}
