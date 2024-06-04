using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Commerce_Attribute
    {
        [Key]
        [ForeignKey("commerces")]
        public int coa_com_id { get; set; }
        [Key]
        [ForeignKey("attributes")]
        public short coa_atr_id { get; set; }
        public char coa_value { get; set; }

    }
}
