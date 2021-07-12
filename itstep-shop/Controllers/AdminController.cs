using itstep_shop.Models;
using itstep_shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> EditProduct(int Id)
        {
            await _ctx.Categories.LoadAsync();
            ViewData["Categories"] = await _ctx.Categories.ToListAsync();
            var product = await _ctx.Products.FirstOrDefaultAsync(product => product.Id == Id);

            return View(product);
        }


        // По хорошему конечно должна быть EditProductViewModel
        public async Task<IActionResult> SaveEditedProduct(Product editedProduct)
        {
            await _ctx.Categories.LoadAsync();
            await _ctx.Products.LoadAsync();

            Product product = await _ctx.Products.SingleOrDefaultAsync(p => p.Id == editedProduct.Id);

            if(string.IsNullOrEmpty(editedProduct.Name) ||
                product.CategoryId == null ||
                string.IsNullOrEmpty(editedProduct.ImageUri))
            {
                return RedirectToAction("EditProduct", editedProduct);
            }

            Category category = _ctx.Categories.SingleOrDefault(c => c.Id == editedProduct.CategoryId);

            product.Name = editedProduct.Name;
            product.Category = category;
            product.ImageUri = editedProduct.ImageUri;

            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }


}
