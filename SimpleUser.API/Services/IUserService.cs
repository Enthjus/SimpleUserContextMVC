using SimpleUser.Domain.Entities;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Services
{
    public interface IUserService
    {
        Task<User> FindByIdAsync(int id);
        User FindById(int id);
        Task<UserDto> FindUserDtoByIdAsync(int id);
        Task<int> InsertAsync(UserCreateDto userCreateDto);
        Task<int> UpdateAsync(UserUpdateDto userUpdateDto);
        Task DeleteAsync(int id);
        bool IsUserAlreadyExistsByEmail(string email, int id = 0);
        Task<PaginatedList<UserDto>> FindAllByPageAsync(int pageSize,  int pageIndex, string filter);
        Task<User> Login(string email, string password);
    }
}
