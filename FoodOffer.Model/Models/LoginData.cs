using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Model.Models
{
    public class LoginData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Salt { get; set; }
    }
}
