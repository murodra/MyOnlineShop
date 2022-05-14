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
    public class CustomerRepository : EFRepository<Customer>, ICustomerRepository
    {
        private readonly MyOnlineShopDbContext _db;

        public CustomerRepository(MyOnlineShopDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }
        public bool Delete(Customer customer)
        {
            throw new NotImplementedException();
        }


        public Customer Login(string email, string password)
        {
            return _db.Customers.FirstOrDefault(
                x=> x.Email == email && 
                x.Password == password);
        }

    }
}
