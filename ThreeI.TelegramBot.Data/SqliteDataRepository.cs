using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class SqliteDataRepository : IDataRepository
    {
        private readonly SqliteDbContext _db;

        public SqliteDataRepository(SqliteDbContext db)
        {
            _db = db;
        }

        public DialogState AddDialogState(DialogState dialogState)
        {
            _db.DialogStates.Add(dialogState);
            _db.SaveChanges();

            return dialogState;
        }

        public DialogState GetDialogStateById(string userId)
        {
            var dialog = _db.DialogStates.FirstOrDefault(d => d.UserId == userId);
            _db.SaveChanges();

            return dialog;
        }

        public DialogState UpdateDialogState(DialogState dialogState)
        {
            var entity = _db.DialogStates.Attach(dialogState);
            entity.State = EntityState.Modified;

            return dialogState;
        }
    }
}
