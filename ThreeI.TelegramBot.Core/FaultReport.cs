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
        public DialogState DialogState { get; set; }

        [Column("date_logged")]
        public DateTime DateLogged { get; set; }
    }
}
