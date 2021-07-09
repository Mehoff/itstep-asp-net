using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using itstep_shop.Models;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Products()
        {
            _ctx.Categories.Load();
            return View(_ctx.Products.ToList());
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
            //var product = await _ctx.Products.FirstOrDefaultAsync(prod => prod.Id == Id);
            //if (product != null)
            //{
            //    return PartialView(product);
            //}
            //else return PartialView("Failed to load product");
        }

        public IActionResult GetMessage()
        {
            return PartialView("Hello World! I am partial view");
        }
    }
}
