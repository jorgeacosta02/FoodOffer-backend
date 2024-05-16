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
    public class Db_User
    {
        [Key]
        public int usr_id { get; set; }
        [MaxLength(60)]
        public string usr_name { get; set; }

        [ForeignKey("identification_types")]
        public short? usr_ide_cod { get; set; }
        [MaxLength(20)]
        public string? usr_ide_num { get; set;}
        [MaxLength(100)]
        public string usr_mail { get; set; }
        [MaxLength(20)]
        public string? usr_phone { get; set; }
        [MaxLength(20)]
        public string? usr_cell_phone { get; set; }


        [ForeignKey("users_type")]
        public char usr_ust_cod { get; set; }
    }
}
