using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Users.Models;
using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Users.Queries
{
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserViewModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public GetUserDetailsQueryHandler(IOnlineShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserViewModel>(await _context.Users
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken));

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return user;
        }
    }
}
