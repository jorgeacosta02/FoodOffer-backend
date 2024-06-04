using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Advertising_Image
    {
        [Key]
        [ForeignKey("advertisings")]
        public int adi_adv_id { get; set; }
        [Key]
        public short adi_item { get; set; }
        [MaxLength(50)]
        public string adi_name { get; set; }
        [MaxLength(150)]
        public string adi_path { get; set; }
    }
}
