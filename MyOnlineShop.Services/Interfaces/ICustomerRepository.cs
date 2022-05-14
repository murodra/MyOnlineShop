using Microsoft.EntityFrameworkCore.Query;
using MyOnlineShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyOnlineShop.Services.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Get(int id);
        Customer Get(Expression<Func<Customer, bool>> expression = null, Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>> includes = null);
        bool Add(Customer customer);
        bool Update(Customer customer);
        bool Delete(Customer customer);
        Customer Login(string email, string password);
    }
}
