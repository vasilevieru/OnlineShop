using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Application.Exceptions;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Users.Models;
using OnlineShop.Domain.Entities;
using OnlineShop.Application.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Users.Queries
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, UserLoginModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;
        private AppSettings _appSettings;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public LoginUserQueryHandler(
            IOnlineShopDbContext context,
            IMediator mediator, IMapper mapper,
            IOptions<AppSettings> options,
            ITokenGenerator tokenGenerator,
            IAccessTokenGenerator accessTokenGenerator)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = options.Value;
            _tokenGenerator = tokenGenerator;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<UserLoginModel> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return null;

            var user = await _context.Users
                 .SingleOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request);
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            var refreshToken = _tokenGenerator.GenerateToken();

            var existsToken = await _context.RefreshTokens
                .Where(x => x.UserId == user.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (existsToken == null)
            {
                var refreshT = new RefreshToken
                {
                    UserId = user.Id,
                    Token = refreshToken,
                    Expires = DateTime.UtcNow.AddDays(5)
                };
                _context.RefreshTokens.Add(refreshT);
            }
            else if (existsToken.Expires < DateTime.UtcNow)
            {
                existsToken.Token = refreshToken;
                existsToken.Expires = DateTime.UtcNow.AddDays(5);
                _context.RefreshTokens.Update(existsToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            string secret = _appSettings.Secret;

            var userLoginModel = _mapper.Map<UserLoginModel>(user);

            userLoginModel.Token = _accessTokenGenerator.GenerateAccessToken(secret, userLoginModel);
            userLoginModel.RefreshToken = refreshToken;

            return userLoginModel;
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
