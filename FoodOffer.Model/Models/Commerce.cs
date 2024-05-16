using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Commerce
    {
        public int Id { get; set; }
        public int Owner_Id { get; set; }
        public int Name { get; set; }
        public Address Address { get; set; }
        public List<Attribute> Attributes { get; set; }
        public short Type { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        public string? Cell_Phone { get; set; }
        public string? Web_Url { get; set; }
        public Image? Logo { get;}
    }
}
