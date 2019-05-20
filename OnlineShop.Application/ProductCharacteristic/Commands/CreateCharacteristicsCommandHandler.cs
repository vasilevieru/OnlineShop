using AutoMapper;
using MediatR;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.ProductCharacteristic.Models;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.ProductCharacteristic.Commands
{
    public class CreateCharacteristicsCommandHandler : IRequestHandler<CreateCharacteristicsCommand, ProductCharacteristicsModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public CreateCharacteristicsCommandHandler(IOnlineShopDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductCharacteristicsModel> Handle(CreateCharacteristicsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateCharacteristicsCommand, ProductCharacteristics>(request);

            _context.ProductCharacteristics.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductCharacteristics, ProductCharacteristicsModel>(entity);
        }
    }
}
