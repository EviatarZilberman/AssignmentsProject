using AssignmentsProject_2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Security;
using System.Security.Claims;
using Utilities;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace AssignmentsProject_2.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        public static User? StaticUser { get; set; } = null;
        private SignInManager<ApplicationUser> SignInManager { get; set; }
        private UserManager<ApplicationUser> UserManager { get; set; }
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            List<User> users = new List<User>();
            DBManager<User>.Instance(MongoStuff.Databases.AssignmentsProject_2.ToString())
                .LoadAll(MongoStuff.Collections.Users.ToString(), out users);
            for (int i = 0; i < users.Count; i++)
            {
                if (user.Email.Equals(users[i].Email) && user.Password.Equals(users[i].Password))
                {
                    AccountController.StaticUser = new User(users[i]);
                    users[i].KeepLoggedIn = user.KeepLoggedIn;
                    AccountController.StaticUser.KeepLoggedIn = user.KeepLoggedIn;
                    List<Claim>? claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, AccountController.StaticUser.ToString()),
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = user.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);
                    LogWriter.Instance().WriteLog("Login", $"Login as {AccountController.StaticUser.UserName}");
                    return RedirectToAction("Index", "Home", user);
                }
            }
            ViewBag.Message = "User not found";
            LogWriter.Instance().WriteLog("Login", $"Login failed as Email: {user.Email}");
            return View();
        }
    }
}