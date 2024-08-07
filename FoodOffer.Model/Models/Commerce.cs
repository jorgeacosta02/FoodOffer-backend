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
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Attribute> Attributes { get; set; }
        public short Type { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }
        public string? WebUrl { get; set; }
        public AppImage? Logo { get; set; }

        public Commerce()
        {
            Addresses = new List<Address>();
            Attributes = new List<Attribute>();
        }
    }
}
