using AutoMapper;
using NuGet.Common;
using SimpleUser.MVC.Auths;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SimpleUser.MVC.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public UserService(HttpClient httpClient, IMapper mapper)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7037/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _mapper = mapper;
        }

        public async Task<JwtToken> SignInAsync(LoginViewModel model)
        {
            LoginDto loginDto = _mapper.Map<LoginDto>(model);
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/v1/Accounts/SignIn", loginDto);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var result = await httpResponseMessage.Content.ReadFromJsonAsync<JwtToken>();
                return result;
            }
            return null;
        }
    }
}
