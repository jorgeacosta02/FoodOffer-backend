using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Advertising_Attribute
    {
        [Key]
        [ForeignKey("advertisings")]
        public int ada_adv_id { get; set; }
        [Key]
        [ForeignKey("attributes")]
        public short ada_atr_cod { get; set; }
        public char ada_value { get; set; }

    }
}
