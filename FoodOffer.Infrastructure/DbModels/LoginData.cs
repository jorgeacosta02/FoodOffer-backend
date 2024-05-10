using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOffer.Infrastructure.DbModels
{
    public class LoginDataDb
    {
        [Key]
        [ForeignKey("users")]
        public int log_usr_id { get; set; } 
        public string log_email { get; set; }
        public string log_pwd { get; set; }
    }
}
