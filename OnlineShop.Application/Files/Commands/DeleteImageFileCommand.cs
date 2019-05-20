using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Files.Commands
{
    public class DeleteImageFileCommand : IRequest
    {
        public int Id { get; set; }
    }
}
