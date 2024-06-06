
namespace FoodOffer.Model.Models
{
    public class Country
    {
        public short Code { get; set; }
        public string? Description { get; set; }

        public Country() {}

        public Country(short code, string description)
        {
            Code = code;
            Description = description;  
        }   

    }
}
