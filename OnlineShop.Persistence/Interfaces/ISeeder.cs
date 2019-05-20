using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Persistence.Interfaces
{
    public interface ISeeder<T> where T : DbContext
    {
        void Seed(T DbContext);
    }
}
