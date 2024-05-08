using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Repositories
{
    public interface IUserRepository
    {
        User GetUser(short userId);
        User GetUser2(short userId);
        List<User> GetUsers();
        bool CreateUser(User data);
        User InsertUser(User data);
    }
}
