using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Files.Models;
using OnlineShop.Application.Interfaces;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;



namespace OnlineShop.Application.Files.Queries
{
    public class GetFilesGroupedByProductQueryHandler : IRequestHandler<GetFilesGroupedByProductQuery, IEnumerable<FilesGroupedByProductViewModel>>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public GetFilesGroupedByProductQueryHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FilesGroupedByProductViewModel>> Handle(GetFilesGroupedByProductQuery request, CancellationToken cancellationToken)
        {
            var files = await _context.Files
                .GroupBy(x => x.ProductId)
                .Select(group => new FilesGroupedByProductViewModel
                {
                    ProductId = group.Key.Value,
                    Files = _mapper.Map<IEnumerable<Image>>(group.ToList())
                })
                .ToListAsync();

            return files;
        }
    }
}
