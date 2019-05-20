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
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DeleteFileCommandHandler(IOnlineShopDbContext context, IMapper mapper, IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<Unit> Handle(DeleteFileCommand message, CancellationToken cancellationToken)
        {
            await HandleDeleteFileCommand(message.Id, p => _fileService.DeleteAsync(p));
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
