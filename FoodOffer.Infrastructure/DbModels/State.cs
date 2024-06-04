using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_State
    {
        [Key]
        public short ste_cod { get; set; }
        [MaxLength(50)]
        public string ste_desc { get; set; }
    }
}
