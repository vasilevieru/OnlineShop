using AutoMapper;
using MediatR;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Files.Commands
{
    public class DeleteImageFileCommandHandler : IRequestHandler<DeleteImageFileCommand>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageFileService _imageFileService;

        public DeleteImageFileCommandHandler(IOnlineShopDbContext context, IMapper mapper, IImageFileService imageFileService)
        {
            _context = context;
            _mapper = mapper;
            _imageFileService = imageFileService;
        }

        public async Task<Unit> Handle(DeleteImageFileCommand request, CancellationToken cancellationToken)
        {
            await HandleDeleteFileCommand(request.Id, p => _imageFileService.DeleteAsync(p));
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private async Task HandleDeleteFileCommand(int id, Func<string, Task> deleteFromStorage)
        {
            Image file = _context.Files.SingleOrDefault(x => x.Id == id);

            if (file == null)
                throw new NotFoundException(nameof(Image), id);

            await deleteFromStorage(file.Path);

            _context.Files.Remove(file);
        }
    }
}
