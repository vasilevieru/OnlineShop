using MediatR;
using OnlineShop.Application.Files.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Files.Queries
{
    public class GetFilesGroupedByProductQuery : IRequest<IEnumerable<FilesGroupedByProductViewModel>>
    {
    }
}
