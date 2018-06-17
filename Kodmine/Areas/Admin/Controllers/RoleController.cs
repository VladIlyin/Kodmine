using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kodmine.Areas.Admin.Models;
using Kodmine.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            var isRoleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!isRoleExists)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                await _roleManager.CreateAsync(role);
            }

            return Json(true);
        }

        public async Task<IActionResult> AddRoleToUser(string userName, string roleName)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(userName);

            if (!await _userManager.IsInRoleAsync(appUser, roleName))
            {
                var res = await _userManager.AddToRoleAsync(appUser, roleName);
                return Json(res.Succeeded);
            }

            return Json(false);
        }

    }
}