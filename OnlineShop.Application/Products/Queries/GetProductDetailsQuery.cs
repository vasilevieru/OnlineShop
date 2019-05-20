using MediatR;
using OnlineShop.Application.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Products.Queries
{
    public class GetProductDetailsQuery : IRequest<ProductDetailsModel>
    {
        public int Id { get; set; }
    }
}
