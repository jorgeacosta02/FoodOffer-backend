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
        public AdvertisingState State { get; set; }
        public Category Category { get; set; }
        public Commerce Commerce { get; set; }
        public List <Attribute> Attributes { get; set; }
        public List<Image> Images { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Advertising()
        {
            Attributes = new List<Attribute>();
            Images = new List<Image>();
            Commerce = new Commerce();
            Category = new Category();
            State = new AdvertisingState();
        }
    }
}
