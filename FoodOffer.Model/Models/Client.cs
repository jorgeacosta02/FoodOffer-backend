using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Define el modelo User con sus propiedades. Es el modelo que va a ser utilizado para manipular la info y
// enviar al front.


namespace FoodOffer.Model.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Address? Address { get; set; }

    }
}
