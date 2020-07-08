using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThreeI.TelegramBot.Core
{
    public class ConversationState
    {
        [Key]
        public int Id { get; set; }
        public string Block { get; set; }
        public string Unit { get; set; }
        public int Option { get; set; }
        public string Description { get; set; }
        public int Confirmation { get; set; }
        public int ChatPhase { get; set; }

        public void Reset()
        {
            Block = string.Empty;
            Unit = string.Empty;
            Option = 0;
            Description = string.Empty;
            Confirmation = 0;
            ChatPhase = 1;
        }
    }
}