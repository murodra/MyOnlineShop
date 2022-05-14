using MyOnlineShop.Data;
using MyOnlineShop.Data.Entities;
using MyOnlineShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyOnlineShop.Services.Concrete
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        private readonly MyOnlineShopDbContext _db;

        public UserRepository(MyOnlineShopDbContext dbContext):base(dbContext)
        {
            _db = dbContext;
        }
        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Login(string email, string password)
        {
            return _db.Users.FirstOrDefault(
                x=> x.Email == email && 
                x.Password == password);
        }
    }
}
