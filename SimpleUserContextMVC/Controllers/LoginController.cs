using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.Models;
using SimpleUser.MVC.Services;
using System.IdentityModel.Tokens.Jwt;

namespace SimpleUser.MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginController(UserManager<IdentityUser> userManager, IUserService userService, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var jwtToken = await _userService.SignInAsync(model);
            HttpContext.Response.Cookies.Append("JWToken", jwtToken.AccessToken.Token, new CookieOptions { 
                HttpOnly = true,
                Expires = jwtToken.AccessToken.Expiration
            }); 
            HttpContext.Response.Cookies.Append("RefreshToken", jwtToken.RefreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Expires = jwtToken.RefreshToken.Expiration
            });
            var token = new JwtSecurityToken(jwtToken.AccessToken.Token);
            var claims = token.Claims;
            string email = claims.FirstOrDefault(u => u.Type == "Email")?.Value;
            if (email != null)
            {
                //var identityUser = new IdentityUser { Email = email };
                //await _signInManager.CreateUserPrincipalAsync(identityUser);
                //var claim = new Claim ( "SomeClaim", "SomeValue" );
                //await _userManager.AddClaimAsync(identityUser, claim);
            
                //await _signInManager.SignInAsync(identityUser, false);
                return RedirectToAction("index", "home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
        }
    }
}
