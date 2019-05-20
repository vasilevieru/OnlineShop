using AutoMapper;
using MediatR;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Products.Models;
using OnlineShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Products.Queries
{
    public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public GetProductDetailsQueryHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<ProductDetailsModel> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            return _mapper.Map<Product, ProductDetailsModel>(entity);
        }
    }
}
