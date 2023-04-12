using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.DTOs;
using SimpleUser.Persistence.Data;

namespace SimpleUser.MVC.Services
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
        }

        public async Task<User> FindByIdAsync(int id)
        {
            User user = await _context.Users.Include(x => x.UserDetail).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IList<UserDto>> FindAsync(string keyword)
        {
            var users = await _context.Users.Include(x => x.UserDetail)
                .Where(u => u.UserDetail.LastName.ToUpper().Contains(keyword.ToUpper()) ||
                u.UserDetail.FirstName.ToUpper().Contains(keyword.ToUpper()) ||
                u.UserDetail.PhoneNumber.Contains(keyword) ||
                u.Email.ToUpper().Contains(keyword.ToUpper()))
                .ToListAsync();
            IList<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;
        }

        public async Task<UserDto> FindUserDtoByIdAsync(int id)
        {
            User user = await FindByIdAsync(id);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<int> InsertAsync(UserDto userDto)
        {
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            User user = _mapper.Map<User>(userDto);
            var entry = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task<int> UpdateAsync(UserDto userDto)
        {
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            var oldUser = await FindByIdAsync(userDto.Id);
            var userToUpdate = _mapper.Map<User>(userDto);
            var entry = _context.Users.Update(oldUser);
            entry.CurrentValues.SetValues(userToUpdate);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }
    }
}
