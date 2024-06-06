
namespace FoodOffer.Model.Models
{
    public class State
    {
        public short Code { get; set; }
        public string? Description { get; set; }

        public State() { }

        public State(short code, string description)
        {
            Code = code;
            Description = description;
        }

    }
}
