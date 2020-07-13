using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeI.TelegramBot.Core
{
    [Table("supervisors")]
    public class Supervisor
    {
        [Key]
        [Column("supervisor_id")]
        public int Id { get; set; }

        [Column("full_name")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Column("chat_id")]
        public long ChatId { get; set; }

        [Column("telegram_user_id")]
        public int TelegramUserId { get; set; }

        public Category Category { get; set; }
    }
}