using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Attribute
    {
        [Key]
        public short atr_cod { get; set; }

        [ForeignKey("attribute_categories")]
        public short atr_atc_cod { get; set; }
        [MaxLength(50)]
        public string atr_desc { get; set; }
    }
}
