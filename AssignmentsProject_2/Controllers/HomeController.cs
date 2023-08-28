using AssignmentsProject_2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities;

namespace AssignmentsProject_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(User? u)
        {
            u = AccountController.StaticUser;
            
            return View(u);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Logout(User? u)
        {
            u = AccountController.StaticUser;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            LogWriter.Instance().WriteLog("Logout", $"Logout as {AccountController.StaticUser.UserName}");

            return RedirectToAction("Login", "Account");
        }
    }
}