using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ThreeI.TelegramBot.Core
{
    [Table("fault_reports")]
    public class FaultReport
    {
        [Key]
        [Column("report_id")]
        public int ReportId { get; set; }

        [ForeignKey("dialog_id")]
        public virtual DialogState DialogState { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("block")]
        public string Block { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("category")]
        public int Category { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("date_logged")]
        public DateTime DateLogged { get; set; }
    }
}
