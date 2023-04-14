using SimpleUser.Domain.Entities;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Services
{
    public interface IUserService
    {
        Task<IList<UserDto>> FindAllAsync();
        Task<User> FindByIdAsync(int id);
        Task<UserDto> FindUserDtoByIdAsync(int id);
        Task<UserUpdateDto> FindUserUpdateByIdAsync(int id);
        Task<IList<UserDto>> FindAsync(string keyword);
        Task<int> InsertAsync(UserCreateDto userCreateDto);
        Task<int> UpdateAsync(UserUpdateDto userUpdateDto);
        Task DeleteAsync(int id);
        bool IsUserAlreadyExistsByEmail(string email);
        Task<PaginatedList<UserDto>> FindAllByPageAsync(int pageSize,  int pageIndex, string filter);
    }
}
