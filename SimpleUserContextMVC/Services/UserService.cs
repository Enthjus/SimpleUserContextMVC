using AutoMapper;
using Newtonsoft.Json;
using NuGet.Common;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SimpleUser.MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private HttpClient _httpClient;
        string token;

        public UserService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7037/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/v1/Users/{id}");
        }

        public async Task<UserDto> FindUserDtoByIdAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/v1/Users/{id}");
            UserDto userDto = null;
            if (response.IsSuccessStatusCode)
            {
                userDto = await response.Content.ReadFromJsonAsync<UserDto>();
            }
            return userDto;
        }

        public async Task<ValidationErrorDto> InsertAsync(UserCreateDto userCreateDto)
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

        public async Task<ValidationErrorDto> UpdateAsync(UserUpdateDto userUpdateDto)
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

        public async Task<UserUpdateDto> FindUserUpdateByIdAsync(int id)
        {
            var user = await FindUserDtoByIdAsync(id);
            UserUpdateDto userUpdate = _mapper.Map<UserUpdateDto>(user);
            return userUpdate;
        }

        public bool IsNullOrZero(int? num)
        {
            return num == 0 || num == null;
        }

        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/v1/Login", loginDto);
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var result = await httpResponseMessage.Content.ReadAsStringAsync();
                token = result.TrimEnd('"').TrimStart('"');
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                }
                return true;
            }
            return false;
        }

        public async Task<PaginatedList<UserDto>> FindAllAsync(IndexVM indexVM)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(
                    $"api/v1/Users?PageSize={indexVM.PageSize}&PageIndex={indexVM.PageIndex}&Filter={indexVM.Filter}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<PaginatedList<UserDto>>();
            }
            return null;
        }
    }
}
