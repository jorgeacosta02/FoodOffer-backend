using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Infrastructure.DbModels
{
    public class Address
    {
        [Key]
        [Column(Order = 0)]
        public short Client_Id { get; set; }
        [Key]
        [Column(Order = 1)]
        public short Item { get; set; }
        public string Street { get; set; }
        public short Number { get; set; }
        public short Floor { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
