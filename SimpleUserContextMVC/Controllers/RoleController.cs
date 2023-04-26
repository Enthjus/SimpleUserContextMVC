using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleUser.MVC.Core;

namespace SimpleUser.MVC.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
        public IActionResult Manager()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin")]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.SuperAdmin)]
        public IActionResult SuperAdmin()
        {
            return View();
        }
    }
}
