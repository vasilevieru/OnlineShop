using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Products.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductListViewModel>
    {

        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductListViewModel> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(x => x.Images)
                .ToListAsync();

            var model = new ProductListViewModel
            {
                Products = _mapper.Map<IEnumerable<ProductDetailsModel>>(products)
            };

            return model;
        }
    }
}
