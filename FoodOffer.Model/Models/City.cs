
namespace FoodOffer.Model.Models
{
    public class City
    {
        public short Code { get; set; }
        public string? Description { get; set; }

        public City() {}

        public City(short code, string description)
        {
            Code = code;
            Description = description;  
        }   

    }
}
