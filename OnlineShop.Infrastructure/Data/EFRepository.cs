using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Domain;
using OnlineShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Infrastructure.Data
{

    public class EFRepository : IRepository
    {
        protected readonly OnlineShopDbContext _dbContext;
        private readonly ICurrentUser currentUser;

        public EFRepository(OnlineShopDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            this.currentUser = currentUser;
        }

        public T Add<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Add(entity);
            SaveChanges();
            return entity;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            SaveChanges();
        }

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>();
        }
        
        public T GetById<T>(int id) where T : BaseEntity
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        
    }
}
