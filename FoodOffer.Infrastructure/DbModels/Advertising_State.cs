using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Advertising_State
    {
        [Key]
        public short ads_cod { get; set; }
        [MaxLength(20)]
        public string ads_desc { get; set; }
    }
}
