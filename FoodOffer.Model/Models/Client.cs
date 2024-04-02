﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }

    }
}
