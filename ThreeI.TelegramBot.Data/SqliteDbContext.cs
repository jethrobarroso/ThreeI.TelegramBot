using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

            modelBuilder.Entity<Supervisor>()
                .HasData(PopulateSupervisors());

            modelBuilder.Entity<Category>()
                .HasData(PopulateCategories());
        }

        private IEnumerable<Supervisor> PopulateSupervisors()
        {
            List<Supervisor> list = new List<Supervisor>();

            for (int i = 0; i < 6; i++)
                list.Add(new Supervisor() { Id = i + 1 });

            return list;
        }

        private IEnumerable<Category> PopulateCategories()
        {
            List<Category> list = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Electricity",
                    Description = "Electrical related issues",
                    SupervisorId = 1,
                },
                new Category()
                {
                    Id = 2,
                    Name = "Plumbing",
                    Description = "Water related issues",
                    SupervisorId = 2,
                },
                new Category()
                {
                    Id = 3,
                    Name = "Paint",
                    Description = "Painting related issues",
                    SupervisorId = 3,
                },
                new Category()
                {
                    Id = 4,
                    Name = "Walls & Ceilings",
                    Description = "Walls & Ceilings related issues",
                    SupervisorId = 4,
                },
                new Category()
                {
                    Id = 5,
                    Name = "Carpentry",
                    Description = "Carpentry",
                    SupervisorId = 5,
                },
                new Category()
                {
                    Id = 6,
                    Name = "Other",
                    Description = "Other",
                    SupervisorId = 6,
                },
            };

            return list;
        }
    }
}
