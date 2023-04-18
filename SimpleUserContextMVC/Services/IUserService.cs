using SimpleUser.Domain.Entities;
using SimpleUser.MVC.DTOs;

namespace SimpleUser.MVC.Services
{
    public interface IUserService
    {
        //Task<PaginatedList<UserDto>> FindAllAsync();
        //Task<User> FindByIdAsync(int id);
        Task<UserDto> FindUserDtoByIdAsync(int id);
        Task<UserUpdateDto> FindUserUpdateByIdAsync(int id);
        //Task<IList<UserDto>> FindAsync(string keyword);
        Task<int> InsertAsync(UserCreateDto userCreateDto);
        Task<int> UpdateAsync(UserUpdateDto userUpdateDto);
        Task DeleteAsync(int id);
        //bool IsUserAlreadyExistsByEmail(string email);
        bool IsNullOrZero(int num);
    }
}
