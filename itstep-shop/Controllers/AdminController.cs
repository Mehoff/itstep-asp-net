using itstep_shop.Models;
using itstep_shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itstep_shop.Controllers
{

    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationContext _ctx;

        public AdminController(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult AccessDenied()
        //{
        //    return View();
        //}

        public IActionResult AddProduct()
        {
            // ???
            ViewData["Categories"] = _ctx.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            //await _ctx.Products.AddAsync(product);
            return RedirectToAction("Index", "Admin");
        }
    }


}
