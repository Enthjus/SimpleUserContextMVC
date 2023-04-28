using SimpleUser.MVC.Areas.Identity.Data;
using SimpleUser.MVC.Auths;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.ViewModels;

namespace SimpleUser.MVC.Services
{
    public interface ICustomerService
    {
        #region CallApi
        Task<CustomerDto> FindUserDtoByIdAsync(int id);
        Task<CustomerUpdateDto> FindUserUpdateByIdAsync(int id);
        Task<ValidationErrorDto> InsertAsync(CustomerCreateDto userCreateDto);
        Task<ValidationErrorDto> UpdateAsync(CustomerUpdateDto userUpdateDto);
        Task DeleteAsync(int id);
        bool IsNullOrZero(int? num);
        Task<JwtToken> LoginAsync(LoginDto loginDto);
        Task<PaginatedList<CustomerDto>> FindAllAsync(IndexVM indexVM);
        #endregion
    }
}
