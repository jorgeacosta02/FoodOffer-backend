using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Declara el modelo de la base de datos, con el nombre de las columnas de la tabla.

namespace FoodOffer.Infrastructure.DbModels
{
    public class UserDb
    {
        public int usr_id { get; set; }
        public string usr_name { get; set; }

        [ForeignKey("identification_types")]
        public short usr_ide_cod { get; set; }
        public string usr_ide_num { get; set; }
        public string usr_mail { get; set; }
        public string usr_phone { get; set; }
        public string usr_cell_phone { get; set; }


        [ForeignKey("users_type")]
        public string usr_ust_cod { get; set; }
    }
}
