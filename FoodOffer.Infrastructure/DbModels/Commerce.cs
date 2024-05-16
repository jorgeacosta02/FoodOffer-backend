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
    public class Db_Commerce
    {
        [Key]
        public int com_id { get; set; }

        [ForeignKey("users")]
        public int com_usr_id { get; set; }

        [ForeignKey("commerce_types")]
        public short com_cot_cod { get; set; }
        [MaxLength(100)]
        public string? com_mail { get; set; }
        [MaxLength(20)]
        public string? com_phone { get; set; }
        [MaxLength(20)]
        public string? com_cell_phone { get; set; }
        [MaxLength(60)]
        public string? com_web_url { get; set; }

    }
}
