﻿using FoodOffer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Services
{
    public interface IUserService
    {
        Client GetUser(short userId);
    }
}
