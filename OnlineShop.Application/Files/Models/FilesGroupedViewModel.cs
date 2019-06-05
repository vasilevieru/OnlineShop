using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Files.Models
{
    public class FilesGroupedViewModel
    {
        public IEnumerable<FilesGroupedByProductViewModel> Files { get; set; }
    }
}
