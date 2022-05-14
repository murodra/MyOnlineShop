using MyOnlineShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineShop.Services.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User Get(int id);
        bool Add(User user);
        bool Update(User user);
        bool Delete(User user);
        User Login(string email, string password);
    }
}
