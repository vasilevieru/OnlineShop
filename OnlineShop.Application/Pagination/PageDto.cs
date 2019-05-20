using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Pagination
{
    public class PageDto
    {
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;
        public string Sort { get; set; }
        public string Filter { get; set; }
        public string FilterFields { get; set; }
        public bool Ascending { get; set; }
    }
}
