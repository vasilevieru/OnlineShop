using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Core.Domain
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string MimeType { get; set; }
        public long Length { get; set; }
    }
}
