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
    public class Db_Advertising_address
    {
        [Key]
        public int aad_adv_id { get; set; }
        [Key]
        public int aad_adv_com_id { get; set; }
        [Key]
        public short aad_add_item { get; set; }

    }
}
