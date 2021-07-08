using itstep_shop.Models;
using itstep_shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itstep_shop.Controllers
{
    public class AccountsController : Controller
    {
        private ApplicationContext _ctx;

        public AccountsController(ApplicationContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult Login()
        {
            return View();
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

                _ctx.Users.Add(user);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }
    }
}
