using LiseWoreEmp.Models;
using LiseWoreEmp.Models.ViewModel;
using LiseWoreEmp.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LiseWoreEmp.Controllers
{
    public class AccountController : Controller
    {

        private readonly LiseWoreEmpContext _context;
        
        private readonly IDataProtector _protector;

        public AccountController(LiseWoreEmpContext context, DataSecurityProvider key, IDataProtectionProvider provider)
        {
            _context = context;
            _protector = provider.CreateProtector(key.Key);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(UserListViewModel u)
        {

            try
            {
                var users = _context.UserLists.Where(x => x.EmailAddress == u.EmailAddress).FirstOrDefault();
                if (users != null)
                {
                    TempData["RegisterError"] = "User already exist with this email!";
                    return View(u);
                }
                
                    short maxid;
                    if (_context.UserLists.Any())
                        maxid = Convert.ToInt16(_context.UserLists.Max(x => x.UserId) + 1);
                    else
                        maxid = 1;
                    u.UserId = maxid;


                    UserList userList = new()
                    {
                        UserId = u.UserId,
                        EmailAddress = u.EmailAddress,
                        UserPassword = _protector.Protect(u.UserPassword)
                    };


                    _context.Add(userList);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Account");

                
     
            }
            catch
            {
                TempData["RegisterError"] = "User Registration Failed. Please try again!";
                return View(u);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserListViewModel uEdit)
        {
            var users = _context.UserLists.ToList();
            if (users != null)
            {

                var u = users.Where(x => x.EmailAddress.ToUpper().Equals(uEdit.EmailAddress.ToUpper()) && _protector.Unprotect(x.UserPassword).Equals(uEdit.UserPassword)).FirstOrDefault();
                if (u != null)
                {
                    List<Claim> claims = new()
                    {
                        new Claim(ClaimTypes.Name,u.UserId.ToString()),
                        new Claim("Email",u.EmailAddress),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(identity));

                    return RedirectToAction("Dashboard");

                }
            }

            TempData["LoginError"] = "Invalid email or password.";
            return View(uEdit);
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
