using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Address
    {
        [Key]
        public int add_ref_id { get; set; }
        [Key]
        public char add_ref_type { get; set; }
        [Key]
        public short add_item { get; set; }
        [MaxLength(255)]
        public string add_desc { get; set; }
        public short add_cit_cod { get; set; }
        public short add_ste_cod { get; set; }
        public short add_cou_cod { get; set; }
        [MaxLength(255)]
        public string? add_obs { get; set; }

    }
}
