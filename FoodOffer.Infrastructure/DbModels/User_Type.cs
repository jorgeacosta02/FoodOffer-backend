using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_User_Type
    {
        [Key]
        public short ust_cod { get; set; }
        [MaxLength(20)]
        public string ust_desc { get; set; }
    }
}
