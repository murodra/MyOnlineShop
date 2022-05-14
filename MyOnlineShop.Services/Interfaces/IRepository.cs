using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyOnlineShop.Services.Interfaces
{
    public interface IRepository<T> where T:class
    {
        List<T> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        T Get(int id);
        T Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
