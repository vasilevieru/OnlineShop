using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Files.Models;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Files.Queries
{
    public class FileQueryHandler : IRequestHandler<GetFileQuery, FileViewModel>
    {

        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public FileQueryHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FileViewModel> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            Image file = await _context.Files.SingleOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<FileViewModel>(file);
        }
    }
}
