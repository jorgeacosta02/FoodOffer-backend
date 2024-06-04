using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Attributes_Category
    {
        [Key]
        public short atc_cod { get; set; }
        [MaxLength(50)]
        public string atc_desc { get; set; }
    }
}
