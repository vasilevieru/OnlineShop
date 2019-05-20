using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Cart.Models;
using OnlineShop.Application.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Cart.Queries
{
    public class GetItemCountQueryHandler : IRequestHandler<GetItemCountQuery, ItemCoutViewModel>
    {
        private readonly IOnlineShopDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public GetItemCountQueryHandler(IOnlineShopDbContext context, IMapper mapper, ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<ItemCoutViewModel> Handle(GetItemCountQuery request, CancellationToken cancellationToken)
        {
            var cartId = _context.ShoppingCarts
                .Where(x => x.UserId == _currentUser.UserId).Select(x => x.Id).FirstOrDefault();

            var count = await _context.ShoppingCartItems
                .Where(x => x.ShoppingCartId == cartId).Select(x => x.ProductId).CountAsync();

            return new ItemCoutViewModel { Count = count };
        }
    }
}
