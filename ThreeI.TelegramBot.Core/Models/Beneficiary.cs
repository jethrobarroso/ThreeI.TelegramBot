using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ThreeI.TelegramBot.Core.Models
{
    public class Beneficiary
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellNumber { get; set; }
        public ConversationState Conversation { get; set; }
        
    }
}
