using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kodmine.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IAuthorizationService authorizationService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            if (_signInManager.IsSignedIn(User))
            {
                if ((await _authorizationService.AuthorizeAsync(User, "AdminPolicy")).Succeeded)
                {
                    return View();
                }
                return View("NotAuthorized");
            }
            else
                return RedirectToAction("Login", "Account");

        }
    }
}