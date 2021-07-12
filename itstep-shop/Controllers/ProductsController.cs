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
        public async Task<IActionResult> AddToCart(int Id)
        {
            await _ctx.Users.LoadAsync();
            await _ctx.Products.LoadAsync();

            var product = await _ctx.Products.SingleOrDefaultAsync(p => p.Id == Id);

            var currentUser = await _ctx.Users.SingleOrDefaultAsync(user => user.Id == int.Parse(User.FindFirst("Id").Value));

            _ctx.Carts.Add(new Cart { Product = product, User = currentUser });
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Products", "Products");
        }

        [Authorize]
        public async Task<IActionResult> ClearCart()
        {
            await _ctx.Users.LoadAsync();

            var currentUser = await _ctx.Users.SingleOrDefaultAsync(user => user.Id == int.Parse(User.FindFirst("Id").Value));
            var toRemove = _ctx.Carts.Where((cart) => cart.User.Id == currentUser.Id).ToList();
            _ctx.Carts.RemoveRange(toRemove);

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Cart", "Accounts");
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int Id)
        {
            var cart = await _ctx.Carts.FirstOrDefaultAsync(c => c.Id == Id);
            if(cart != null)
            {
                _ctx.Carts.Remove(cart);
                _ctx.SaveChanges();
            }

            return RedirectToAction("Cart", "Accounts");
        }


    }
}
