using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVTQ.LogifySearch.Application.Interfaces.Services;
using MVTQ.LogifySearch.Domain.Common;
using MVTQ.LogifySearch.Domain.Entities;
using MVTQ.LogifySearch.Domain.Model.Account;

namespace MVTQ.LogifySearch.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<User> signInManager,
            IUserService userService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var roles = await _userManager.GetRolesAsync(user!);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied(string returnUrl)
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageUsers()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromQuery]UserSearchModel filter)
        {
            var users = await _userService.GetUsers(
                q=> (string.IsNullOrEmpty(filter.Search) ||q.UserName.Contains(filter.Search) || q.Email.Contains(filter.Search)),
                q => q.OrderBy(u => u.UserName), 
                string.IsNullOrEmpty(filter.Roles) ? new List<string>() : filter.Roles.Split(",").ToList(), 
                new PaginationFilter { Limit = filter.Limit, Offset = filter.Offset}
                );
            return Json(users);
        }
    }
}
