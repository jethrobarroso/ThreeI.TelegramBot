using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;

namespace ThreeI.TelegramBot.Core
{
    [Table("dialog_states")]
    public class DialogState
    {
        [Key]
        [Column("dialog_id")]
        public int DialogId { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("block")]
        public string Block { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("category")]
        public int Category { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("confirmation")]
        public int Confirmation { get; set; }

        [Column("chat_phase")]
        public int ChatPhase { get; set; }

        [Column("last_active")]
        public DateTime LastActive { get; set; }

        [Column("is_support_mode")]
        public bool IsSupportMode { get; set; }

        public virtual List<FaultReport> FaultReports { get; set; }

        public void Reset(bool withSupport)
        {
            Block = null;
            Unit = null;
            Category = 0;
            Description = null;
            Confirmation = 0;
            ChatPhase = 1;
            LastActive = DateTime.Now;
            IsSupportMode = withSupport;
        }
    }
}