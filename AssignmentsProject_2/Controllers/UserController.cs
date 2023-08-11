using AssignmentsProject_2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Utilities;

namespace AssignmentsProject_2.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // TODO = MAKE ASYNC
        public async Task<IActionResult> Create(User user)
        {
            List<User> users = new List<User>();
             DBManager<User>.Instance(MongoStuff.Databases.AssignmentsProject_2.ToString()).LoadAll(MongoStuff.Collections.Users.ToString(), out users);
            CoreReturns r1 = CoreReturns.WAITING_TO_INITIALIZE, r2 = CoreReturns.WAITING_TO_INITIALIZE;
            for (int i = 0; i < users.Count; i++)
            {
                if (CompareUser(user, users[i]) == CoreReturns.PARAMETERS_EQAUL)
                {
                    ViewBag.Message = "Username or email are being used!";
                    r1 = CoreReturns.PARAMETERS_EQAUL;
                    break;
                }
            }
            string message = string.Empty;
            r2 = ValidPassword(user, out message);
            if (r2 != CoreReturns.SUCCESS)
            {
                ViewBag.Message = message;
            }
            if (r1 != CoreReturns.PARAMETERS_EQAUL && r2 == CoreReturns.SUCCESS)
            {
                CoreReturns result = DBManager<User>
                    .Instance(MongoStuff.Databases.AssignmentsProject_2.ToString())
                    .Insert(MongoStuff.Collections.Users.ToString(), user);
                if (result == CoreReturns.SUCCESS)
                {
                    ViewBag.Message = "User Created Successfully!";
                }
            }
            return View(user);
        }


        /// <summary>
        /// ORIGIN METHOD!!!
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        /*[HttpPost]
        public async Task<IActionResult> Create(User user)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                };
                    IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                await _userManager.AddToRoleAsync(appUser, "Admin");    
                if (result == CoreReturns.SUCCESS)
                        ViewBag.Message = "User Created Successfully!";
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
            }
            return View(user);
        }*/

        /*[HttpPost]
        public async Task<IActionResult> CreateRole(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = userRole.RoleName });
                if (result.Succeeded)
                    ViewBag.Message = "Role Created Successfully!";
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }*/

        public static CoreReturns CompareUser(User u1, User u2)
        {
            if (u1 == null || u2 == null)
            {
                return CoreReturns.IS_NULL;
            }
            CoreReturns f = CoreReturns.NOT_EQUAL;
            if (u1.Email.Equals(u2.Email) || u1.UserName.Equals(u2.UserName))
            {
                f = CoreReturns.PARAMETERS_EQAUL;
            }
            return f;
        }

        public static CoreReturns ValidPassword(User user, out string message)
        {
            if (user == null || user.Password == null)
            {
                message = "Enter a password!";
                return CoreReturns.IS_NULL;
            }

            if (user.Password.Length < 6)
            {
                message = "Password is too short!";
                return CoreReturns.OUT_RETURN;
            }

            bool[] isOk = new bool[4];
            for (int i = 0; i < user.Password.Length; i++)
            {
                if (user.Password[i] >= 'A' && user.Password[i] <= 'Z')
                {
                    isOk[0] = true;
                }
                else if (user.Password[i] >= 'a' && user.Password[i] <= 'z')
                {
                    isOk[1] = true;
                }
                else if (user.Password[i] >= '0' && user.Password[i] <= '9')
                {
                    isOk[2] = true;
                }
                else if (user.Password[i] >= '!' && user.Password[i] <= '/' || user.Password[i] >= ':' && user.Password[i] <= '@')
                {
                    isOk[3] = true;
                }
            }
            if (isOk[0] == isOk[1] == isOk[2] == isOk[3] == true)
            {
                message = null;
                return CoreReturns.SUCCESS;
            }
            else
            {
                message = "Ilegal password!";
                return CoreReturns.PASSWORD_INVALID;
            }
        }
    }
}