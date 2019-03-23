using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Core.Interfaces
{
    public interface ICurrentUser
    {
        string UserName { get; }
    }
}
