using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Advertising
    {
        [Key]
        public int adv_id { get; set; }

        [ForeignKey("commerces")]
        public int adv_com_id { get; set; }
        [MaxLength(150)]
        public string adv_title { get; set; }
        public string adv_desc { get; set; }
        public double adv_price { get; set; }

        [ForeignKey("advertising_states")]
        public char adv_ads_cod { get; set; }

        [ForeignKey("advertising_categories")]
        public short adv_cat_cod { get; set; }
        public DateTime adv_create_data { get; set; }
        public DateTime? adv_delete_data { get; set; }
        public DateTime adv_update_data { get; set; }
    }
}
