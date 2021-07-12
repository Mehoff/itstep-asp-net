using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace itstep_shop.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [DataType(DataType.Text), MinLength(3)]
        public string Name { get; set; }
    }
}
