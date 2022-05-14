using Microsoft.EntityFrameworkCore.Query;
using MyOnlineShop.Data;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyOnlineShop.Services.Concrete
{
    public class EFRepository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly MyOnlineShopDbContext _db;
        public EFRepository(MyOnlineShopDbContext db)
        {
            _db = db;
        }
        public bool Add(T entity)
        {
            entity.IsActive = true;
            entity.CreatedDate = DateTime.Now;

            _db.Set<T>().Add(entity);
            return _db.SaveChanges()>0;
        }

        public bool Delete(int id)
        {
            var entity = this.Get(id);
            if (entity == null)
            {
                return false;
            }
            entity.IsActive = false;
            entity.UpdatedDate = DateTime.Now;
            return _db.SaveChanges() > 0;
        }

        public T Get(int id)
        {
            return _db.Set<T>().FirstOrDefault(x => x.Id == id && x.IsActive);
        }

        public T Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            var query = _db.Set<T>().AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                query = includes(query);
            }
            return query.FirstOrDefault(x=>x.IsActive);
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            var query = _db.Set<T>().AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                query = includes(query);
            }
            return query.Where(x => x.IsActive).ToList();
        }

        public bool Update(T entity)
        {
            _db.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.Set<T>().Update(entity);
            return _db.SaveChanges() > 0;
        }
    }
}
