using OnlineShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Files.Models
{
    public class FilesGroupedByProductViewModel
    {
        public int ProductId { get; set; }
        public IEnumerable<Image> Files { get; set; }
    }
}
