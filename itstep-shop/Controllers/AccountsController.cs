using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Claims;
using System.Threading.Tasks;

using itstep_shop.Models;
using itstep_shop.ViewModels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace itstep_shop.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        private ApplicationContext _ctx;


        public AccountsController(ApplicationContext ctx, ILogger<AccountsController> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken] Почитать что это
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Email = model.Email;
                var Password = model.Password;
                var RememberMe = model.RememberMe; // Пока не используется

                User user = await _ctx.Users.FirstOrDefaultAsync(user => 
                    user.Email == Email && 
                    user.Password == Password
                );

                if(user == null)
                {

                    ModelState.AddModelError("", "Неверный логин или пароль");
                    return View(model);

                    // Login Current User
                    //if (RememberMe)
                    //{
                        // Логика сохранения сессии пользователя надолго
                    //}
                }

                await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(await _ctx.Users.FirstOrDefaultAsync(user => user.Email == model.Email) != null)
                {
                    ModelState.AddModelError("", "Пользователь с такой почтой уже зарегистрирован");
                    return View(model);
                }

                User user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                };


                Role role = await _ctx.Roles.FirstOrDefaultAsync(role => role.Name == "user");
                if (role is not null)
                {
                    user.Role = role;
                }

                await _ctx.Users.AddAsync(user);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(User user)
        {
            await _ctx.Roles.LoadAsync();

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email));
            claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name));
            
            ClaimsIdentity identity = new(
                claims,
                "RozetkaCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
                );

            ClaimsPrincipal principal = new(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public async Task<IActionResult> Cart()
        {
            // ???
            //_ctx.Carts.FirstOrDefaultAsync(c => c.User.Name == User.Identity.Name);
            //_ctx.Users.SingleOrDefaultAsync(u => u.Cart);
            return View();
        }
    }
}
