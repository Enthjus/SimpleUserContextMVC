using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleUserContext.Data;
using SimpleUserContext.Models;
using SimpleUserContextMVC.DTOs;

namespace SimpleUserContext.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public UserService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            User user = await FindByIdAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<UserDto>> FindAllAsync()
        {
            var users = await _context.Users.Include(x => x.UserDetail).AsNoTracking().ToListAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
            //    users.Select(a => new UserDto
            //{
            //    Id = a.Id,
            //    Username = a.Username,
            //    Password = a.Password,
            //    Email = a.Email,
            //    UserDetailDto = new UserDetailDto
            //    {
            //        FirstName = a.UserDetail.FirstName,
            //        LastName = a.UserDetail.LastName,
            //        PhoneNumber = a.UserDetail.PhoneNumber,
            //        Address = a.UserDetail.Address
            //    }
            //}).ToList();
        }

        public async Task<User> FindByIdAsync(int id)
        {
            User user = await _context.Users.Include(x => x.UserDetail).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<UserDto> FindUserDtoByIdAsync(int id)
        {
            User user = await FindByIdAsync(id);
            //UserDetail userDetail = user.UserDetail;
            UserDto userDto = _mapper.Map<UserDto>(user);
            //userDto.Id = user.Id;
            //userDto.Username = user.Username;
            //userDto.Email = user.Email;
            //userDto.Password = user.Password;
            //userDto.UserDetailDto = new UserDetailDto();
            //userDto.UserDetailDto.FirstName = userDetail.FirstName;
            //userDto.UserDetailDto.LastName = userDetail.LastName;
            //userDto.UserDetailDto.PhoneNumber = userDetail.PhoneNumber;
            //userDto.UserDetailDto.Address = userDetail.Address;
            return userDto;
        }

        public async Task<int> InsertAsync(UserDto userDto)
        {
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            User user = _mapper.Map<User>(userDto);
            //user.Username = userDto.Username;
            //user.Email = userDto.Email;
            //user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            //user.UserDetail = new UserDetail();
            //user.UserDetail.FirstName = userDto.UserDetailDto.FirstName;
            //user.UserDetail.LastName = userDto.UserDetailDto.LastName;
            //user.UserDetail.PhoneNumber = userDto.UserDetailDto.PhoneNumber;
            //user.UserDetail.Address = userDto.UserDetailDto.Address;
            var entry = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task<int> UpdateAsync(UserDto userDto)
        {
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var oldUser = await FindByIdAsync(userDto.Id);
            var userToUpdate = _mapper.Map<User>(userDto);
            //userToUpdate.Username = userDto.Username;
            //userToUpdate.Email = userDto.Email;
            //userToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            //userToUpdate.UserDetail.FirstName = userDto.UserDetailDto.FirstName;
            //userToUpdate.UserDetail.LastName = userDto.UserDetailDto.LastName;
            //userToUpdate.UserDetail.PhoneNumber = userDto.UserDetailDto.PhoneNumber;
            //userToUpdate.UserDetail.Address = userDto.UserDetailDto.Address;
            var entry = _context.Users.Update(oldUser);
            entry.CurrentValues.SetValues(userToUpdate);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }
    }
}
