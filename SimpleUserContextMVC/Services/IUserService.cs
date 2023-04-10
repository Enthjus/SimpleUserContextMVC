using SimpleUserContext.Models;
using SimpleUserContextMVC.DTOs;

namespace SimpleUserContext.Services
{
    public interface IUserService
    {
        Task<IList<UserDto>> FindAllAsync();
        Task<User> FindByIdAsync(int id);
        Task<UserDto> FindUserDtoByIdAsync(int id);
        Task<int> InsertAsync(UserDto userVM);
        Task<int> UpdateAsync(UserDto userVM);
        Task DeleteAsync(int id);
    }
}
