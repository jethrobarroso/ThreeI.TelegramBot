using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public class InMemoryData : IDataRepository
    {
        private List<DialogState> _dialogs;

        public InMemoryData()
        {
            _dialogs = new List<DialogState>();
        }

        public DialogState GetDialogById(string userId)
        {
            throw new NotImplementedException();
        }

        public DialogState UpdateDialog(DialogState beneficiary)
        {
            throw new NotImplementedException();
        }
    }
}