using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Advertising
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public double Price { get; set; }
        public Commerce Commerce { get; set; }
        public List <Attribute> Attributes { get; set; }
    }
}
