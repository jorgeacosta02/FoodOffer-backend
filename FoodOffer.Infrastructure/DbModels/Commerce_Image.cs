using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Commerce_Image
    {
        [Key]
        [ForeignKey("commerces")]
        public int coi_com_id { get; set; }
        [Key]
        public short coi_item { get; set; }
        [MaxLength(50)]
        public string coi_name { get; set; }
        [MaxLength(150)]
        public string coi_path { get; set; }
    }
}
