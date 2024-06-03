﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Category
    {
        public short Code { get; set; }
        public string Description { get; set; }

        public Category(short code, string description)
        {
            Code = code;
            Description = description;  
        }   

    }
}
