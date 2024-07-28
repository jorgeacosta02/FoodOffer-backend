using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class AttributeValue
    {
        public int Id { get; set; }
        public short Category { get; set; }
        public string? Description { get; set; }
        public char Value { get; set; }
    }
}
