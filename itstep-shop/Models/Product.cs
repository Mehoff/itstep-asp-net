using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itstep_shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImageUri { get; set; }

        //? 
        // public List<Cart> Carts { get; set; } = new List<Cart>();


    }
}
