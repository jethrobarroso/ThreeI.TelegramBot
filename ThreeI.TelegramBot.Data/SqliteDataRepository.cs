using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class SqliteDataRepository : IDataRepository
    {
        private readonly IServiceScopeFactory _scopeFactor;

        public SqliteDataRepository(IServiceScopeFactory scopeFactory)
        {
            _scopeFactor = scopeFactory;
        }

        public DialogState AddDialogState(DialogState dialogState)
        {
            using (var scope = _scopeFactor.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
                db.DialogStates.Add(dialogState);
                db.SaveChanges();
            }
            return dialogState;
        }

        public FaultReport AddReport(FaultReport fault)
        {
            using (var scope = _scopeFactor.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
                db.FaultReports.Add(fault);
                db.SaveChanges();
                return fault;
            }
        }

        public DialogState GetDialogStateById(string userId)
        {
            using (var scope = _scopeFactor.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
                var dialog = db.DialogStates.FirstOrDefault(d => d.UserId == userId);
                return dialog;
            }
        }

        public DialogState UpdateDialogState(DialogState dialogState)
        {
            using (var scope = _scopeFactor.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
                var entity = db.DialogStates.Attach(dialogState);
                entity.State = EntityState.Modified;
                db.SaveChanges();
                return dialogState;
            }
        }
    }
}
