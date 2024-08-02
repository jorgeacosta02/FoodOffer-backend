
namespace FoodOffer.Model.Models
{
    public class AdvertisingTimeSet
    {
        public int AdvId { get; set; }
        public short Day { get; set; }
        public string Start1 { get; set; } 
        public string End1 { get; set; }
        public char NextDay1 { get; set; }
        public string? Start2 { get; set; }
        public string? End2{ get; set; }
        public char? NextDay2 { get; set; }

    }
}
