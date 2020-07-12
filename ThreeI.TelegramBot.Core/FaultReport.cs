using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeI.TelegramBot.Core
{
    [Table("reports")]
    public class FaultReport
    {
        [Key]
        [Column("report_id")]
        public int Id { get; set; }

        [Column("first_name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Column("last_name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column("block")]
        [Required]
        [StringLength(20)]
        public string Block { get; set; }

        [Column("unit")]
        [Required]
        public string Unit { get; set; }

        [Column("description")]
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Column("date_logged")]
        public DateTime DateLogged { get; set; }


        public virtual DialogState DialogState { get; set; }
        public Category Category { get; set; }
    }
}
