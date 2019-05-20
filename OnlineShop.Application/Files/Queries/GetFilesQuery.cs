using MediatR;
using OnlineShop.Application.Files.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Files.Queries
{
    public class GetFilesQuery : IRequest<FilesViewModel>
    {
        public int ProductId { get; set; }
    }
}
