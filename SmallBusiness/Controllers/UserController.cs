using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmallBusiness.Models;
using SmallBusiness.Models.Repositories;
using SmallBusiness.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmallBusiness.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository repository;

        public UserController(IUserRepository repo)
        {
            repository = repo;
        }

        public ViewResult Login(string returnUrl)
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userinput)
        {
            if (ModelState.IsValid)
            {
                User _user = repository.Users.Include(u => u.UserRole).FirstOrDefault(u => u.Login == userinput.Login && u.Password == userinput.Password);
                if (_user != null)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString()),
                        new Claim("SellerId", _user.Id.ToString())};

                    if (_user.UserRole.IsAdmin)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Seller"));
                    }
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return Redirect("/");
                }
            }
            ModelState.AddModelError("", "The email and/or password entered is invalid. Please try again.");
            ModelState.AddModelError("Login", "");
            ModelState.AddModelError("Password", "");
            return View(new LoginViewModel());
        }
    }
}