using MediatR;
using OnlineShop.Application.Pagination;
using OnlineShop.Application.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Products.Queries
{
    public class GetProductsPageQuery : IRequest<PagedResult<ProductDetailsModel>>
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string Sort { get; set; }
        public string Filter { get; set; }
        public string FilterFields { get; set; }
        public bool Ascending { get; set; }
    }
}
