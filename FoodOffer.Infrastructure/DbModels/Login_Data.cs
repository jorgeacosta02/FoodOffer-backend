using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_User_Login_Data
    {
        [Key]
        [ForeignKey("users")]
        public int uld_usr_id { get; set; }
        [MaxLength(100)]
        public string uld_email { get; set; }
        [MaxLength(100)]
        public string uld_pwd { get; set; }
        [MaxLength(50)]
        public string uld_salt { get; set; }
    }
}
