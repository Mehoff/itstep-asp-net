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
        public IActionResult AddProduct()
        {
            // Best practice? :P
            ViewData["Categories"] = _ctx.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product
                {
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    ImageUri = model.ImageUri
                };

                await _ctx.Products.AddAsync(product);
                await _ctx.SaveChangesAsync();

                // Redirect to newly added product page
                // for now, just redirect to main admin page
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Неверно заполненные данные о продукте");
            return View();
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = model.Name,
                };

                await _ctx.Categories.AddAsync(category);
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Неверно заполненные данные о категории");
            return View();
        }
    }


}
