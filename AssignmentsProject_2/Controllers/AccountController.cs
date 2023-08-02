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
    public class AccountController : Controller
    {
        public static User? StaticUser { get; set; } = null;
        /*  public PassModel Pass { get; set; }

          public AccountController() { }
          public AccountController(PassModel Pass) {
              this.Pass = Pass;
          }
          public AccountController (User u, Assignment a)
          {
              this.Pass = new PassModel { User = u, Assignment = a };
          }*/
        /*        private UserManager<ApplicationUser> _userManager;
                private SignInManager<ApplicationUser> _signInManager;

                public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
                {
                    this._userManager = userManager;
                    this._signInManager = signInManager;
                }*/

        /*   public IActionResult Login()
           {
               return View();
           }*/

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Assignment");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            List<User> users = new List<User>();
            DBManager<User>.Instance(MongoStuff.Databases.AssignmentsProject_2.ToString())
                .LoadAll(MongoStuff.Collections.Users.ToString(), out users);
            User u = new Models.User();
            for (int i = 0; i < users.Count; i++)
            {
                if (user.Email.Equals(users[i].Email) && user.Password.Equals(users[i].Password))
                {
                    u = new User(users[i]);
                    AccountController.StaticUser = u;
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Email),
                        new Claim("OtherProperties", "Example Role")
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
                    return RedirectToAction("Index", "Home", u);
                }
            }
            ViewBag.Message = "User not found";

            return View();
        }


        /*  [HttpPost]
      //[AllowAnonymous]
    //  [ValidateAntiForgeryToken]
      public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string returnurl)
      {
          List<User> users = new List<User>();
          DBManager<User>.Instance(MongoStuff.Databases.AssignmentsProject_2.ToString()).LoadAll(MongoStuff.Collections.Users.ToString(), out users);
          CoreReturns r1 = CoreReturns.WAITING_TO_INITIALIZE;
          for (int i = 0; i < users.Count; i++)
          {
              if (users[i].Email.Equals(email) && users[i].Password.Equals(password))
              {
                  r1 = CoreReturns.SUCCESS;
              return RedirectToAction("Create", "User");
              }
          }
          if (r1 != CoreReturns.SUCCESS)
          {
              ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
          }
          return View();
      }*/

        /// <summary>
        /// ORIGIN METHOD!!!
        /// </summary>
        /// <returns></returns>
        /*[HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password, string returnurl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnurl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or Password");
            }
            return View();
        }*/
        /*
                [Authorize]
                public async Task<IActionResult> Logout()
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }*/
    }
}