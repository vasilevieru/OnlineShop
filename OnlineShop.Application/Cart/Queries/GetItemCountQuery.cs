using MediatR;
using OnlineShop.Application.Cart.Models;

namespace OnlineShop.Application.Cart.Queries
{
    public class GetItemCountQuery : IRequest<ItemCoutViewModel>
    {
    }
}
