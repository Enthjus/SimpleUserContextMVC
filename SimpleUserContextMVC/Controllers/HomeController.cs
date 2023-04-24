using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.DTOs;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.Services;
using System.Diagnostics;

namespace SimpleUser.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Login(LoginDto loginDto) 
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            JwtTokenDto jwtToken = await _userService.LoginAsync(loginDto);
            Response.Cookies.Append("AccessToken", jwtToken.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = jwtToken.ExpirationToken
            });
            Response.Cookies.Append("RefreshToken", jwtToken.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = jwtToken.ExpirationRefreshToken
            });
            if (true)
            {
                return RedirectToAction(controllerName: "Users", actionName: "Index");
            }
            return View(loginDto);
        }
    }
}