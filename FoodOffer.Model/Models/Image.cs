﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Image
    {
        public int Id { get; set; }
        public short Item { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}