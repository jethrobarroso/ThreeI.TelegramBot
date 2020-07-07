using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThreeI.TelegramBot.Core.Models
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
    }
}