using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThreeI.TelegramBot.Core
{
    public class DialogState
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Block { get; set; }
        public string Unit { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
        public int Confirmation { get; set; }
        public int ChatPhase { get; set; }
        public DateTime LastActive { get; set; }
        public bool IsSupportMode { get; set; }

        public void Reset()
        {
            Block = null;
            Unit = null;
            Category = 0;
            Description = null;
            Confirmation = 0;
            ChatPhase = 1;
            LastActive = DateTime.Now;
            IsSupportMode = false;
        }
    }
}