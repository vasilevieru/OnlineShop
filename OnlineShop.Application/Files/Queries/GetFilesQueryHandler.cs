using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Files.Models;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Files.Queries
{
    public class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, FilesViewModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public GetFilesQueryHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<FilesViewModel> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Image> files = await _context.Files
                .Where(x => x.ProductId == request.ProductId).ToListAsync();

            return _mapper.Map<FilesViewModel>(files);
        }
    }
}
