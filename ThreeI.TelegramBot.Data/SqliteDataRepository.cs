using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class SqliteDataRepository : IDataRepository
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SqliteDataRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public DialogState AddDialogState(DialogState dialogState)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            db.DialogStates.Add(dialogState);
            db.SaveChanges();
            return dialogState;
        }

        public FaultReport AddReport(FaultReport fault)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            db.FaultReports.Add(fault);
            db.SaveChanges();
            return fault;
        }

        public Category GetCategoryById(int categoryValue)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            var result = db.Categories
                .Include(c => c.Supervisor)
                .FirstOrDefault(c => c.Id == categoryValue);
            return result;
        }

        public DialogState GetDialogStateById(string userId)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            var dialog = db.DialogStates
                .Include(d => d.Category)
                .Include(d => d.Category.Supervisor)
                .FirstOrDefault(d => d.UserId == userId);
            if (dialog != null)
                dialog.FaultReports = new List<FaultReport>();
            return dialog;
        }

        public Supervisor GetSupervisorByCategory(string category)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            return db.Supervisors.FirstOrDefault(s => s.Category.Name == category);
        }

        public IEnumerable<Supervisor> GetSupervisors()
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            return db.Supervisors;
        }

        public DialogState UpdateDialogState(DialogState dialogState)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            var entity = db.DialogStates.Attach(dialogState);
            entity.State = EntityState.Modified;
            db.SaveChanges();
            return dialogState;
        }

        public Supervisor UpdateSupervisor(Supervisor supervisor)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            var entity = db.Supervisors.Attach(supervisor);
            entity.State = EntityState.Modified;
            db.SaveChanges();
            return supervisor;
        }
    }
}
