using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Infrastructure.DbModels
{
    public class AdvertisingDb
    {
        [Key]
        
        public int Id { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }

    }
}
