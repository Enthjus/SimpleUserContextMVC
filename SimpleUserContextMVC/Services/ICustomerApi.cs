using Refit;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;

namespace SimpleUser.MVC.Services
{
    public interface ICustomerApi
    {
        [Get("/api/v1/Customers")]
        Task<PaginatedList<CustomerDto>> FindAllAsync([Query] IndexViewModel indexVM);
        [Get("/Customers/{id}")]
        Task<CustomerInfoDto> FindByIdAsync(int id);
        [Post("/Customsers")]
        Task CreateCustomerAsync([Body]CustomerCreateDto customerCreate);
        [Put("/Customsers")]
        Task UpdateCustomerAsync([Body] CustomerUpdateDto customerUpdate);
        [Delete("/Customsers/{id}")]
        Task DeleteAsync(int id);
    }
}
