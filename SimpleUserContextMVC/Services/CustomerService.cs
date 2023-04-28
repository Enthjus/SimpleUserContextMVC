using AutoMapper;
using Newtonsoft.Json;
using SimpleUser.MVC.Auths;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.ViewModels;
using System.Net.Http.Headers;

namespace SimpleUser.MVC.Services
{
    public class CustomerService : ICustomerService
    {
        #region CallApi
        private readonly IMapper _mapper;
        private HttpClient _httpClient;
        string token;

        public CustomerService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7037/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/v1/Users/{id}");
        }

        public async Task<CustomerDto> FindUserDtoByIdAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/Users/{id}");
            CustomerDto userDto = null;
            if (response.IsSuccessStatusCode)
            {
                userDto = await response.Content.ReadFromJsonAsync<CustomerDto>();
            }
            return userDto;
        }

        public async Task<ValidationErrorDto> InsertAsync(CustomerCreateDto userCreateDto)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/v1/Users", userCreateDto);
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
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/v1/Users", userUpdateDto);
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

        public async Task<JwtToken> LoginAsync(LoginDto loginDto)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/v1/Auth/login", loginDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var result = await httpResponseMessage.Content.ReadFromJsonAsync<JwtToken>();
                return result;
            }
            return null;
        }

        public async Task<PaginatedList<CustomerDto>> FindAllAsync(IndexVM indexVM)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                    $"api/v1/Users?PageSize={indexVM.PageSize}&PageIndex={indexVM.PageIndex}&Filter={indexVM.Filter}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PaginatedList<CustomerDto>>();
            }
            return null;
        }
        #endregion
    }
}
