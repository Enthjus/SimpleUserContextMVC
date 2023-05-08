using SimpleUser.Domain.Entities;
using SimpleUser.API.DTOs;

namespace SimpleUser.API.Services
{
    public interface ICustomerService
    {
        Task<Customer> FindByIdAsync(int id);
        Customer FindById(int id);
        Task<CustomerInfoDto> FindCustomerDtoByIdAsync(int id);
        Task<int> InsertAsync(CustomerCreateDto CustomerCreateDto);
        Task<int> UpdateAsync(CustomerUpdateDto CustomerUpdateDto);
        Task DeleteAsync(int id);
        bool IsCustomerAlreadyExistsByEmail(string email, int id = 0);
        Task<PaginatedList<CustomerDto>> FindAllByPageAsync(int pageSize,  int pageIndex, string filter);
        Task<Customer> Login(string email, string password);
    }
}
