using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Files.Models;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Files.Commands
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, FileViewModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFileCommandHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FileViewModel> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            Image file = _context.Files.SingleOrDefault(x => x.Id == request.Id);

            if (file == null)
                throw new NotFoundException(nameof(Image), request.Id);

            file.Name = request.Name;
            file.Path = request.Path;
            file.Length = request.Length;
            file.MimeType = request.MimeType;

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileViewModel>(file);
        }
    }
}
