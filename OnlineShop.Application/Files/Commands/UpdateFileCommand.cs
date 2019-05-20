using MediatR;
using OnlineShop.Application.Files.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Files.Commands
{
    public class UpdateFileCommand : IRequest<FileViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string MimeType { get; set; }
        public long Length { get; set; }
        public int? ProductId { get; set; }
    }
}
