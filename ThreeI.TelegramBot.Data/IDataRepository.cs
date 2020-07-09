using System;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public interface IDataRepository
    {
        DialogState GetDialogById(string userId);
        DialogState UpdateDialog(DialogState beneficiary);
    }
}
