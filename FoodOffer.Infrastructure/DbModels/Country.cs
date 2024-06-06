using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Country
    {
        [Key]
        public short cou_cod { get; set; }
        [MaxLength(50)]
        public string cou_desc { get; set; }
    }
}
