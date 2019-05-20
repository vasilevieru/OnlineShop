using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Helpers;
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
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, RefreshTokenModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly ITokenGenerator _tokenGenerator;
        private AppSettings _appSettings;
        private readonly IMapper _mapper;
        IAccessTokenGenerator _accessTokenGenerator;


        public RefreshTokenQueryHandler(
            IOnlineShopDbContext context,
            ITokenGenerator tokenGenerator,
            IAccessTokenGenerator accessTokenGenerator,
            IOptions<AppSettings> options,
            IMapper mapper)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
            _appSettings = options.Value;
            _mapper = mapper;
            _accessTokenGenerator = accessTokenGenerator;
        }
        public async Task<RefreshTokenModel> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _context.RefreshTokens
                 .Where(x => x.Token == request.RefreshToken && x.UserId == request.UserId)
                 .SingleOrDefaultAsync(cancellationToken);

            var user = _mapper.Map<UserLoginModel>(await _context.Users
                 .Where(x => x.Id == request.UserId)
                 .SingleOrDefaultAsync(cancellationToken));

            if (token.Expires < DateTime.UtcNow)
            {
                return null;
            }

            var secret = _appSettings.Secret;

            return new RefreshTokenModel
            {
                AccessToken = _accessTokenGenerator.GenerateAccessToken(secret, user),
                RefreshToken = token.Token
            };
        }
    }
}
