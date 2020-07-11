using System;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public interface IDataRepository
    {
        DialogState GetDialogStateById(string userId);
        DialogState UpdateDialogState(DialogState dialogState);
        DialogState AddDialogState(DialogState dialogState);
        FaultReport AddReport(FaultReport fault);
    }
}
