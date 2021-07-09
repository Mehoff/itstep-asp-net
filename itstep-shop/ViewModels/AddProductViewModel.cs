using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace itstep_shop.ViewModels
{
    public class AddProductViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUri { get; set; }
    }
}
