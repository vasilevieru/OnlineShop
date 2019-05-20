using AutoMapper;
using MediatR;
using OnlineShop.Application.Interfaces;
using OnlineShop.Application.Users.Models;
using OnlineShop.Domain.Entities;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Users.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDetailsModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IOnlineShopDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDetailsModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<RegisterUserCommand, User>(request);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            _context.Users.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<User, UserDetailsModel>(entity);
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)   
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }


}
