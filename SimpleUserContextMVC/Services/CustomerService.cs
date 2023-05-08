using AutoMapper;
using Newtonsoft.Json;
using Refit;
using SimpleUser.MVC.Auths;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;
using System.Net.Http.Headers;

namespace SimpleUser.MVC.Services
{
    public class CustomerService : ICustomerService
    {
        #region CallApi
        private readonly IMapper _mapper;
        private HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        //private HttpContext _httpContext;
        private readonly ICustomerApi _customerApi;

        public CustomerService(HttpClient httpClient, IMapper mapper, IHttpClientFactory httpClientFactory, ICustomerApi customerApi)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://localhost:7037/");
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var hd = _httpContext.Request.Headers.Authorization;
            _mapper = mapper;
            _httpClientFactory = httpClientFactory;
            _customerApi = customerApi;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/v1/Users/{id}");
        }

        public async Task<CustomerDto> FindUserDtoByIdAsync(int id)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/v1/Customers/{id}");
            HttpResponseMessage response = await _httpClient.SendAsync(httpRequest);
            CustomerDto userDto = null;
            if (response.IsSuccessStatusCode)
            {
                userDto = await response.Content.ReadFromJsonAsync<CustomerDto>();
            }
            return userDto;
        }

        public async Task<ValidationErrorDto> InsertAsync(CustomerCreateDto userCreateDto)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/v1/Customers", userCreateDto);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            ValidationErrorDto errorDto = new ValidationErrorDto();
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                errorDto.Id = int.Parse(result);
                return errorDto;
            }
            errorDto = JsonConvert.DeserializeObject<ValidationErrorDto>(result);
            return errorDto;
        }

        public async Task<ValidationErrorDto> UpdateAsync(CustomerUpdateDto userUpdateDto)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/v1/Customers", userUpdateDto);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            ValidationErrorDto errorDto = new ValidationErrorDto();
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                errorDto.Id = int.Parse(result);
                return errorDto;
            }
            errorDto = JsonConvert.DeserializeObject<ValidationErrorDto>(result);
            return errorDto;
        }

        public async Task<CustomerUpdateDto> FindUserUpdateByIdAsync(int id)
        {
            var user = await FindUserDtoByIdAsync(id);
            CustomerUpdateDto userUpdate = _mapper.Map<CustomerUpdateDto>(user);
            return userUpdate;
        }

        public bool IsNullOrZero(int? num)
        {
            return num == 0 || num == null;
        }

        public async Task<PaginatedList<CustomerDto>> FindAllAsync(IndexViewModel indexVM)
        {
            //HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, $"api/v1/Customers?PageSize={indexVM.PageSize}&PageIndex={indexVM.PageIndex}&Filter={indexVM.Filter}");
            return await _customerApi.FindAllAsync(indexVM);
        }
        #endregion
    }
}
