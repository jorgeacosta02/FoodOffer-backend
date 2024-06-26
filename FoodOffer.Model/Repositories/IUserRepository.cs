﻿using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Define la interface del repositorio de User estableciendo los métodos que como mínimo tienen que 
// definirse en la clase la implementa.

namespace FoodOffer.Model.Repositories
{
    public interface IUserRepository
    {
        User GetUser(short userId);
        User GetUserByIdSimple(short userId);
        User GetUserCompleteByEmail(string email);
        User GetUserByEmail(string email);
        List<User> GetUsers();
        bool CreateUser(User data);
        User InsertUser(User data);
        User UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
