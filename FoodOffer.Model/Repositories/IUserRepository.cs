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
        Client GetUser(short userId);
        Client GetUser2(short userId);
    }
}
