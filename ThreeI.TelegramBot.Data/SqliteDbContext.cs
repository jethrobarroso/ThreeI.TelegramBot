using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options) { }
        
        public DbSet<DialogState> DialogStates { get; set; }
        public DbSet<FaultReport> FaultReports { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DialogState>()
        //        .HasAlternateKey(d => d.UserId);

        //    modelBuilder.Entity<FaultReport>();
        //}
    }
}
