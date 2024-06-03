using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class AdvertisingState
    {
        public char Code { get; set; }
        public string Description { get; set; }

        public AdvertisingState(char code, string description)
        {
            Code = code;
            Description = description;  
        }   

    }
}
