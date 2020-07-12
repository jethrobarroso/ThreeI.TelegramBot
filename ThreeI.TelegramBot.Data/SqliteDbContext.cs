using Microsoft.EntityFrameworkCore;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }

        public DbSet<DialogState> DialogStates { get; set; }
        public DbSet<FaultReport> FaultReports { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supervisor>()
                .HasOne(s => s.Category)
                .WithOne(c => c.Supervisor)
                .HasForeignKey<Category>(c => c.SupervisorId);
        }
    }
}
