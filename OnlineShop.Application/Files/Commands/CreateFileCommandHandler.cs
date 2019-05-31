using AutoMapper;
using MediatR;
using OnlineShop.Application.Files.Models;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Files.Commands
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, FileViewModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public CreateFileCommandHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FileViewModel> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var file = _mapper.Map<Image>(request);

            await _context.Files.AddAsync(file);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileViewModel>(file);
        }
    }
}
