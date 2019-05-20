using MediatR;
using OnlineShop.Application.Files.Models;

namespace OnlineShop.Application.Files.Queries
{
    public class GetFileQuery : IRequest<FileViewModel>
    {
        public int Id { get; set; }
    }
}
