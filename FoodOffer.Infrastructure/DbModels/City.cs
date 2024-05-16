using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_City
    {
        [Key]
        public short cit_cod { get; set; }

        [ForeignKey("states")]
        public short cit_ste_cod { get; set; }
        [MaxLength(50)]
        public string cit_desc { get; set; }
    }
}
