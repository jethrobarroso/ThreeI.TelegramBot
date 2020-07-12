using System.Collections.Generic;
using ThreeI.TelegramBot.Core;

namespace ThreeI.TelegramBot.Data
{
    public interface IDataRepository
    {
        DialogState GetDialogStateById(string userId);
        DialogState UpdateDialogState(DialogState dialogState);
        DialogState AddDialogState(DialogState dialogState);
        FaultReport AddReport(FaultReport fault);
        Category GetCategoryById(int categoryValue);
        IEnumerable<Supervisor> GetSupervisors();
        Supervisor GetSupervisorByCategory(string category);
        Supervisor UpdateSupervisor(Supervisor supervisor);
    }
}
