using AutoMapper;
using MediatR;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Products.Models;
using OnlineShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDetailsModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IOnlineShopDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailsModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateProductCommand, Product>(request);

            _context.Products.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Product, ProductDetailsModel>(entity);
        }
    }
}
