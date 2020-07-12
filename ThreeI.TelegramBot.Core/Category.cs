using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThreeI.TelegramBot.Core
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Column("supervisor_id")]
        public int SupervisorId { get; set; }
        public Supervisor Supervisor { get; set; }
        public List<DialogState> Dialog { get; set; }
        public List<FaultReport> Report { get; set; }
    }
}