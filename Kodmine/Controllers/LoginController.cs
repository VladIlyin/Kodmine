using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.Controllers
{
    public class LoginController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AuthUser authUser)
        {
            var username = "admin"; //Configuration["username"];
            var password = "123"; //Configuration["password"];
            if (authUser.Username == username && authUser.Password == password)
            {
                var claims = new[] { new Claim("name", "admin"), new Claim(ClaimTypes.Role, "Admin") };
                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.Authentication.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                  new ClaimsPrincipal(identity));

                return Redirect("~/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Login failed. Please check Username and/or password");
            }

            //Thread.CurrentPrincipal = HttpContext.User;

            return View();
        }

    }

    public class AuthUser
    {
        public string Username { get; internal set; }
        public string Password { get; internal set; }
    }
}