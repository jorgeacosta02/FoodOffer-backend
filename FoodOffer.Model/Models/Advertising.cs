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
        public Client ClientData { get; set; }
        public List <Client> Properties { get; set; }
    }
}
