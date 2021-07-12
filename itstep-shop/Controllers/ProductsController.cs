using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using itstep_shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using itstep_shop.Classes;

namespace itstep_shop.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationContext _ctx;

        public ProductsController(ApplicationContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public IActionResult Products(string searchString, string CategoryId)
        {
            var products = _ctx.Products.ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where((p) => p.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(CategoryId))
            {
                int categoryId = int.Parse(CategoryId);
                products = products.Where(product => product.CategoryId == categoryId).ToList();
            }

            ViewData["CategoryId"] = new SelectList(_ctx.Categories.ToList(), "Id", "Name");
            return View(products);
        }

        public async Task<IActionResult> GetProduct(int Id)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(prod => prod.Id == Id);
            if (product != null)
            {
                return PartialView(product);
            }
            else return PartialView("Failed to load product");
        }

        public IActionResult GetProduct(Product Product)
        {
            return PartialView(Product);
        }

        public IActionResult ProductPage(int id)
        {
            var product = _ctx.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _ctx.Categories.Load();
                return View(product);
            }
            else return View("Нет продукта с таким Id");
        }

        public IActionResult GetMessage()
        {
            return PartialView("Hello World! I am partial view");
        }

        [Authorize]
        public IActionResult AddToCart()
        {
            return View();
        }

        //public IActionResult SelectProductsWithCategory(int Id)
        //{
        //    _ctx.Categories.Load();
        //    ViewData["Categories"] = _ctx.Categories.ToList();
        //    return View(_ctx.Products.Where(product => product.Id == Id).ToList());
        //}

    }
}
