using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class Address
    {
        public short Client_Id { get; set; }
        public short Item { get; set; }
        public string Street { get; set; }
        public short Number { get; set; }
        public short Floor { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
