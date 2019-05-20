using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Products.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductDetailsModel> Products { get; set; }
    }
}
