using SimpleUser.Domain.Entities;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Services
{
    public interface IUserService
    {
        Task<IList<UserDto>> FindAllAsync();
        Task<User> FindByIdAsync(int id);
        Task<UserDto> FindUserDtoByIdAsync(int id);
        Task<IList<UserDto>> FindAsync(string keyword);
        Task<int> InsertAsync(UserDto userVM);
        Task<int> UpdateAsync(UserDto userVM);
        Task DeleteAsync(int id);
    }
}
