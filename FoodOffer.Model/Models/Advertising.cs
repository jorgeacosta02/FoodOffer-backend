
namespace FoodOffer.Model.Models
{
    public class Advertising
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public double Price { get; set; }
        public short StateCode { get; set; }
        public short CategoryCode { get; set; }
        public Commerce Commerce { get; set; }
        public List <AttributeValue> Attributes { get; set; }
        public List<AppImage> Images { get; set; }
        public List<AdvertisingTimeSet> AdvertisingTimeSets { get; set; }
        public short PriorityLevel { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Advertising()
        {
            Attributes = new List<AttributeValue>();
            Images = new List<AppImage>();
            Commerce = new Commerce();
        }
    }
}
