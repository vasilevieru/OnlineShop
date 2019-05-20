using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Pagination;
using OnlineShop.Application.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Products.Queries
{
    public class GetProductsPageQueryHandler : IRequestHandler<GetProductsPageQuery, PagedResult<ProductDetailsModel>>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _mapperConfiguration;

        public GetProductsPageQueryHandler(IOnlineShopDbContext context, IMapper mapper, IConfigurationProvider configurationProvider)
        {
            _context = context;
            _mapper = mapper;
            _mapperConfiguration = configurationProvider;
        }


        public async Task<PagedResult<ProductDetailsModel>> Handle(GetProductsPageQuery request, CancellationToken cancellationToken)
        {
            var query = (await _context.Products.ToListAsync()).AsQueryable();
            var dto = _mapper.Map<PageDto>(request);

            var result = PagedResult<ProductDetailsModel>.From(query, dto, _mapperConfiguration);

            return result;
        }
    }
}
