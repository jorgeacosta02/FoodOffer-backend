using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace clasificados.Infraestructure.DbContextConfig.DbModels
{
    public class Db_Advertising_Time_Set
    {
        [Key]
        [ForeignKey("advertisings")]
        public int ats_adv_id { get; set; }
        [Key]
        public short ats_day { get; set; }
        public DateTime ats_start_1 { get; set; }
        public DateTime ats_end_1 { get; set; }
        public char? ats_nextday_1 { get; set; }
        public DateTime? ats_start_2 { get; set; }
        public DateTime? ats_end_2 { get; set; }
        public char? ats_nextday_2 { get; set; }

    }
}
