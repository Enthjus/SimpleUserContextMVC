using SimpleUser.MVC.Areas.Identity.Data;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.ViewModels;

namespace SimpleUser.MVC.Services
{
    public interface IUserService
    {
        #region CallApi
        Task<UserDto> FindUserDtoByIdAsync(int id);
        Task<UserUpdateDto> FindUserUpdateByIdAsync(int id);
        Task<ValidationErrorDto> InsertAsync(UserCreateDto userCreateDto);
        Task<ValidationErrorDto> UpdateAsync(UserUpdateDto userUpdateDto);
        Task DeleteAsync(int id);
        bool IsNullOrZero(int? num);
        Task<JwtTokenDto> LoginAsync(LoginDto loginDto);
        Task<PaginatedList<UserDto>> FindAllAsync(IndexVM indexVM);
        #endregion
    }
}
