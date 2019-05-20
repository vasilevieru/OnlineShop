using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Products.Models;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Products.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDetailsModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IOnlineShopDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailsModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            product = _mapper.Map<Product>(request);

            _context.Products.Update(product);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDetailsModel>(product);
        }
    }
}
