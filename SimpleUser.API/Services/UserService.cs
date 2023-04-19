using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleUser.Domain.Entities;
using SimpleUser.API.DTOs;
using SimpleUser.Persistence.Data;
using Microsoft.VisualBasic;
using AutoMapper.QueryableExtensions;
using System.Runtime.CompilerServices;

namespace SimpleUser.API.Services
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

        public async Task<User> FindByIdAsync(int id)
        {
            User user = await _context.Users.Include(x => x.UserDetail).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<UserDto> FindUserDtoByIdAsync(int id)
        {
            User user = await FindByIdAsync(id);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<int> InsertAsync(UserCreateDto userCreateDto)
        {
            userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            User user = _mapper.Map<User>(userCreateDto);
            var entry = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task<int> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var oldUser = await FindByIdAsync(userUpdateDto.Id);
            var newUser = _mapper.Map<User>(userUpdateDto);
            if (string.IsNullOrEmpty(newUser.Password))
            {
                newUser.Password = oldUser.Password;
            }
            else
            {
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            }
            var entry = _context.Users.Update(oldUser);
            entry.CurrentValues.SetValues(newUser);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public bool IsUserAlreadyExistsByEmail(string email, int id = 0)
        {
            if(id != 0)
            {
                var oldUser = _context.Users.Find(id);
                if(oldUser.Email != email)
                {
                    return _context.Users.Any(x => x.Email == email);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return _context.Users.Any(x => x.Email == email);
            }
        }

        public async Task<PaginatedList<UserDto>> FindAllByPageAsync(int pageSize, int pageIndex, string filter)
        {
            IQueryable<User> users = from u in _context.Users.Include(x => x.UserDetail)
                                     select u;
            IQueryable<UserDto> userDtos = _mapper.ProjectTo<UserDto>(users);
            PaginatedList<UserDto> pageList;
            if (!string.IsNullOrEmpty(filter))
            {
                userDtos = userDtos
                    .Where(u => u.UserDetailDto.LastName.ToUpper().Contains(filter.ToUpper()) ||
                    u.UserDetailDto.FirstName.ToUpper().Contains(filter.ToUpper()) ||
                    u.UserDetailDto.PhoneNumber.Contains(filter) ||
                    u.Email.ToUpper().Contains(filter.ToUpper()));
            }
            if(IsZeroOrNull(pageSize))
            {
                pageSize = 4;
            }
            if(IsZeroOrNull(pageIndex))
            {
                pageIndex = 1;
            }
            pageList = await PaginatedList<UserDto>.CreateAsync(userDtos, pageIndex, pageSize);
            return pageList;
        }

        public bool IsZeroOrNull(int? num)
        {
            if(num == 0 || num == null) return true;
            return false;
        }

        public User FindById(int id)
        {
            User user = _context.Users.Include(x => x.UserDetail).FirstOrDefault(u => u.Id == id);
            return user;
        }
    }
}
