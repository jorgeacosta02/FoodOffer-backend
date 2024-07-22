using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Category
    {
        public short Type { get; set; }
        public short Code { get; set; }
        public string? Description { get; set; }

        public Category() {}

        public Category(short code, string description)
        {
            Code = code;
            Description = description;  
        }   

    }
}
